using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Dtos.Expense;
using Microsoft.ServiceFabric.Services.Remoting;


namespace Contract.Services
{
    public interface IExpenseService : IService
    {
        Task<List<ExpenseDto>> getAllExpenses(int travelId);

        Task<ExpenseSummaryDto> getExpenseSummary(int travelId, decimal budget);

        Task<bool> deleteExpense(int id);

        Task<bool> updateExpense(int id, UpdateExpenseDto dto);

        Task<ExpenseDto> createExpense(CreateExpenseDto dto, int travelPlanId);

        Task<bool> deleteExpensesByTravelPlan(int travelPlanId);
    }
}
