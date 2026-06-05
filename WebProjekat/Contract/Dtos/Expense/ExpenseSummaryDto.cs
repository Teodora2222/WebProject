using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dtos.Expense
{
    public class ExpenseSummaryDto
    {
        public decimal TotalExpenses { get; set; }
        public decimal RemainingBudget { get; set; }
        public List<ExpenseDto> Expenses { get; set; } = new List<ExpenseDto>();
    }
}
