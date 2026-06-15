using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Contract.Dtos.Expense;
using Contract.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace ExpenseService
{
    internal sealed class ExpenseServiceHost : StatefulService, IExpenseService
    {
        private readonly IServiceProvider serviceProvider;
        public ExpenseServiceHost(StatefulServiceContext context, IServiceProvider serviceProvider)
            : base(context)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<ExpenseDto> createExpense(CreateExpenseDto dto,int travelPlanId)
        {
            var result =
                await ExecuteAsync<IExpenseService, ExpenseDto>(
                    s => s.createExpense(dto, travelPlanId));

            var queue = await StateManager
                .GetOrAddAsync<IReliableQueue<string>>(
                    "expenseAuditQueue");

            using var tx = StateManager.CreateTransaction();

            await queue.EnqueueAsync(
                tx,
                $"Created expense '{dto.Name}' amount={dto.Amount}");

            await tx.CommitAsync();

            return result;
        }

        public async Task<bool> deleteExpense(int id)
        {
            var result =
                await ExecuteAsync<IExpenseService, bool>(
                    s => s.deleteExpense(id));

            if (result)
            {
                var queue = await StateManager
                    .GetOrAddAsync<IReliableQueue<string>>(
                        "expenseAuditQueue");

                using var tx = StateManager.CreateTransaction();

                await queue.EnqueueAsync(
                    tx,
                    $"Deleted expense id={id}");

                await tx.CommitAsync();
            }

            return result;
        }

        public Task<List<ExpenseDto>> getAllExpenses(int travelId)
        {
            return ExecuteAsync<IExpenseService, List<ExpenseDto>>(s => s.getAllExpenses(travelId));
        }

        public Task<ExpenseSummaryDto> getExpenseSummary(int travelId, decimal budget)
        {
            return ExecuteAsync<IExpenseService, ExpenseSummaryDto>(s => s.getExpenseSummary(travelId, budget));
        }

        public async Task<bool> updateExpense(int id,UpdateExpenseDto dto)
        {
            var result =
                await ExecuteAsync<IExpenseService, bool>(
                    s => s.updateExpense(id, dto));

            if (result)
            {
                var queue = await StateManager
                    .GetOrAddAsync<IReliableQueue<string>>(
                        "expenseAuditQueue");

                using var tx = StateManager.CreateTransaction();

                await queue.EnqueueAsync(
                    tx,
                    $"Updated expense id={id}");

                await tx.CommitAsync();
            }

            return result;
        }

        public Task<bool> deleteExpensesByTravelPlan(int travelPlanId)
        {
            return ExecuteAsync<IExpenseService, bool>(s => s.deleteExpensesByTravelPlan(travelPlanId));
        }

        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return this.CreateServiceRemotingReplicaListeners();
        }

        private async Task<TResult> ExecuteAsync<TService, TResult>(Func<TService, Task<TResult>> action)
        {
            using var scope = serviceProvider.CreateScope();
            var svc = scope.ServiceProvider.GetRequiredService<TService>();
            return await action(svc);
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            var auditQueue = await StateManager
                .GetOrAddAsync<IReliableQueue<string>>("expenseAuditQueue");

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                using (var tx = StateManager.CreateTransaction())
                {
                    var item = await auditQueue.TryDequeueAsync(tx);
                    if (item.HasValue)
                    {
                        Console.WriteLine($"[Audit] {item.Value}");
                        await tx.CommitAsync();
                    }
                    else
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                    }
                }
            }
        }

    }
}