using Contract.Dtos.Expense;
using Contract.Services;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace Gateway.Services
{

    public class ServiceFabricExpenseGatewayService : IExpenseGatewayService
    {
        private IValidatorService CreateProxy() =>
            ServiceProxy.Create<IValidatorService>(new Uri("fabric:/WebProject/ValidatorService"));

        public Task<List<ExpenseDto>> GetAllExpensesAsync(int travelId) =>
            CreateProxy().GetAllExpenses(travelId);

        public Task<ExpenseSummaryDto> GetExpenseSummaryAsync(int travelId, decimal budget) =>
            CreateProxy().GetExpenseSummary(travelId, budget);

        public Task<ExpenseDto?> CreateExpenseAsync(CreateExpenseDto dto, int travelPlanId) =>
            CreateProxy().CreateExpense(dto, travelPlanId);

        public Task<bool> UpdateExpenseAsync(int id, UpdateExpenseDto dto) =>
            CreateProxy().UpdateExpense(id, dto);

        public Task<bool> DeleteExpenseAsync(int id) =>
            CreateProxy().DeleteExpense(id);
    }
}
