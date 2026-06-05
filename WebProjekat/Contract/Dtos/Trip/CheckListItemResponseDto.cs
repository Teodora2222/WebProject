using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dtos.Trip
{
    public class CheckListItemResponseDto
    {
        public int Id { get; set; }

        public int TravelPlanId { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}
