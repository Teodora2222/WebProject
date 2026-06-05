using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dtos.Trip
{
    public class ChecklistItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsCompleted { get; set; }
    }
}
