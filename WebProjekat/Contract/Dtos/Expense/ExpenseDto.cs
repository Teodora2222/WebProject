using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dtos.Expense
{
    public class ExpenseDto
    {
        public int id { get; set; }
        public int travelPlanId { get; set; }
        public string name { get; set; } = "";
        public string category { get; set; } = "";
        public decimal amount { get; set; }
        public DateTime date { get; set; }
        public string? description { get; set; }
    }
}
