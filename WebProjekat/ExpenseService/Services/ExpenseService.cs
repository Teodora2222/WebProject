using Contract.Dtos.Expense;
using Contract.Services;
using ExpenseService.Data;
using ExpenseService.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpenseService.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext context;

        public ExpenseService(AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        public async Task<ExpenseDto> createExpense(CreateExpenseDto dto, int travelPlanId)
        {
            var databaseEntity = new Expense
            {
                travelPlanId = travelPlanId,
                name = dto.Name,
                category = dto.Category,
                amount = dto.Amount,
                date = dto.Date,
                description = dto.Description
            };

            await context.Expenses.AddAsync(databaseEntity);
            await context.SaveChangesAsync();

            return new ExpenseDto
            {
                id = databaseEntity.id,
                travelPlanId = databaseEntity.travelPlanId,
                name = databaseEntity.name,
                category = databaseEntity.category,
                amount = databaseEntity.amount,
                date = databaseEntity.date,
                description = databaseEntity.description ?? ""
            };
        }

        public async Task<bool> deleteExpense(int id)
        {
            var expense = await context.Expenses.FirstOrDefaultAsync(e => e.id == id);
            if (expense == null) return false;

            context.Expenses.Remove(expense);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ExpenseDto>> getAllExpenses(int travelId)
        {
            return await context.Expenses
                .Where(e => e.travelPlanId == travelId)
                .Select(e => new ExpenseDto
                {
                    id = e.id,
                    travelPlanId = e.travelPlanId,
                    name = e.name,
                    category = e.category,
                    amount = e.amount,
                    date = e.date,
                    description = e.description ?? ""
                })
                .ToListAsync();
        }

        public async Task<ExpenseSummaryDto> getExpenseSummary(int travelId, decimal budget)
        {
            var expenses = await context.Expenses
               .Where(e => e.travelPlanId == travelId)
                .Select(e => new ExpenseDto
                {
                    id = e.id,
                    travelPlanId = e.travelPlanId,
                    name = e.name,
                    category = e.category,
                    amount = e.amount,
                    date = e.date,
                    description = e.description ?? ""
                })
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
