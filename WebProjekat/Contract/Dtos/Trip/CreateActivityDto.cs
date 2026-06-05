using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dtos.Trip
{
    public class CreateActivityDto
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? Time { get; set; }

        public string? Location { get; set; }

        public DateTime Date { get; set; }

        public string status { get; set; } = "PLANNED";
        public decimal? EstimatedCost { get; set; }
    }
}
