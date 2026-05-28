using System.Fabric;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace ExpenseService
{
    internal sealed class ExpenseServiceHost : StatefulService
    {
        public ExpenseServiceHost(StatefulServiceContext context)
            : base(context) { }

        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new ServiceReplicaListener[0];
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