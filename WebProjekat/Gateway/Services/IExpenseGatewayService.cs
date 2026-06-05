using Contract.Dtos.Expense;

namespace Gateway.Services
{
    public interface IExpenseGatewayService
    {
        Task<List<ExpenseDto>> GetAllExpensesAsync(int travelId);
        Task<ExpenseSummaryDto> GetExpenseSummaryAsync(int travelId, decimal budget);
        Task<ExpenseDto?> CreateExpenseAsync(CreateExpenseDto dto, int travelPlanId);
        Task<bool> UpdateExpenseAsync(int id, UpdateExpenseDto dto);
        Task<bool> DeleteExpenseAsync(int id);
    }
}
