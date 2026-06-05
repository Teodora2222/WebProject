using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dtos.Trip
{
    public class ActivityDto
    {
        public int Id { get; set; }

        public int TravelPlanId { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Time { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal? EstimatedCost { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
