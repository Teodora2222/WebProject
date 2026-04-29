using ExpenseService.Domain.Models;

namespace ExpenseService.Domain.DTOs
{
    public class ExpenseSummaryDto
    {
        public decimal TotalExpenses { get; set; }
        public decimal RemainingBudget { get; set; }
        public List<Expense> Expenses { get; set; } = new();
    }
}
