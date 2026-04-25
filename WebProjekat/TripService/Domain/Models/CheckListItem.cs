using System.ComponentModel.DataAnnotations;

namespace TripService.Domain.Models
{
    public class ChecklistItem
    {
        [Key]
        public int Id { get; set; }
        public int TravelPlanId { get; set; }

        [Required]
        [MaxLength(200)]
        public string name { get; set; } = null!;

        public bool isCompleted { get; set; } = false;
    }
}
