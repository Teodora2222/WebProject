using Contract.Dtos.Expense;
using ValidatorService.Clients;

namespace ValidatorService.Validators
{
    public class ExpenseValidator
    {
        private readonly ExpenseServiceClient expenseClient;

        public ExpenseValidator(ExpenseServiceClient expenseClient)
        {
            this.expenseClient = expenseClient;
        }

        public async Task<ExpenseDto?> CreateExpense(
            CreateExpenseDto dto,
            int travelPlanId)
        {
            if (dto.Amount <= 0)
                return null;

            if (string.IsNullOrWhiteSpace(dto.Name))
                return null;

            return await expenseClient
                .CreateProxy()
                .createExpense(dto, travelPlanId);
        }

        public async Task<bool> UpdateExpense(
            int id,
            UpdateExpenseDto dto)
        {
            if (id <= 0)
                return false;

            return await expenseClient
                .CreateProxy()
                .updateExpense(id, dto);
        }

        public async Task<bool> DeleteExpense(int id)
        {
            if (id <= 0)
                return false;

            return await expenseClient
                .CreateProxy()
                .deleteExpense(id);
        }

        public async Task<List<ExpenseDto>> GetAllExpenses(
            int travelId)
        {
            return await expenseClient
                .CreateProxy()
                .getAllExpenses(travelId);
        }

        public async Task<ExpenseSummaryDto> GetExpenseSummary(
            int travelId,
            decimal budget)
        {
            return await expenseClient
                .CreateProxy()
                .getExpenseSummary(travelId, budget);
        }
    }
}