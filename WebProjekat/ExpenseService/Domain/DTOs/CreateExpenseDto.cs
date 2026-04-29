using ExpenseService.Domain.Enums;

namespace ExpenseService.Domain.DTOs
{
    public class CreateExpenseDto
    {
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
    }
}
