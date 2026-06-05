using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dtos.Trip
{
    public class SharedTravelPlanDto
    {
        public int Id { get; set; }

        public int TravelPlanId { get; set; }

        public string Token { get; set; } = string.Empty;
         
        public string Permission { get; set; } = string.Empty;

        public DateTime? CreatedAt { get; set; }

        public DateTime? ExpiresAt { get; set; }
    }
}
