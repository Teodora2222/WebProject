using ExpenseService.Data;
using ExpenseService.Domain.DTOs;
using ExpenseService.Domain.Models;
using ExpenseService.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ExpenseService.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext context;

        public ExpenseService(AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        public async Task<Expense> createExpense(CreateExpenseDto dto, int travelPlanId)
        {
            Console.WriteLine($"=== travelPlanId: {travelPlanId}");
            var expense = new Expense
            {
                name = dto.Name,
                category = dto.Category,
                amount = dto.Amount,
                date = dto.Date,
                description = dto.Description,
                travelPlanId = travelPlanId
            };
            Console.WriteLine($"=== expense.travelPlanId: {expense.travelPlanId}");
            await context.Expenses.AddAsync(expense);
            await context.SaveChangesAsync();
            return expense;
        }

        public async Task<bool> deleteExpense(int id)
        {
            var expense = await context.Expenses.FirstOrDefaultAsync(e => e.id == id);
            if (expense == null) return false;

            context.Expenses.Remove(expense);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Expense>> getAllExpenses(int travelId)
        {
            return await context.Expenses
                .Where(e => e.travelPlanId == travelId)
                .ToListAsync();
        }

        public async Task<ExpenseSummaryDto> getExpenseSummary(int travelId, decimal budget)
        {
            var expenses = await context.Expenses
                    .Where(e => e.travelPlanId == travelId)
                    .ToListAsync();

            var total = expenses.Sum(e => e.amount);

            return new ExpenseSummaryDto
            {
                TotalExpenses = total,
                RemainingBudget = budget - total,
                Expenses = expenses
            };
        }

        public async Task<bool> updateExpense(int id, UpdateExpenseDto dto)
        {
            var expense = await context.Expenses.FirstOrDefaultAsync(e => e.id == id);
            if(expense != null)
            {
                expense.name = dto.Name ?? expense.name;
                expense.date = dto.Date ?? expense.date;
                expense.description = dto.Description ?? expense.description;
                expense.amount = dto.Amount ?? expense.amount;
                expense.category = dto.Category ?? expense.category;

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
