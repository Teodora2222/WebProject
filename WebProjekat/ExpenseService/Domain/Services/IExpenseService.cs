using System.Diagnostics;
using ExpenseService.Domain.DTOs;
using ExpenseService.Domain.Models;

namespace ExpenseService.Domain.Services
{
    public interface IExpenseService
    {
        public Task<List<Expense>> getAllExpenses(int travelId);

        public Task<ExpenseSummaryDto> getExpenseSummary(int travelId,decimal budget);

        public Task<bool> deleteExpense(int id);

        public Task<bool> updateExpense(int id, UpdateExpenseDto dto);

        public Task<Expense> createExpense(CreateExpenseDto dto, int travelPlanId);
    }
}
