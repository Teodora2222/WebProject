using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dtos.Trip
{
    public class CreateDestinationDto
    {
        public string Name { get; set; } = string.Empty;
        
        public string? Description { get; set; }

        public string? Location { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Note { get; set; }
    }
}
