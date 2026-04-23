using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripService.Domain.Models
{

    public class TravelPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int userId { get; set; }

        [Required]
        [MaxLength(100)]
        public string title { get; set; } = null!;  

        [MaxLength(100)]
        public string? description { get; set; }   

        [Required]
        public DateTime startDate { get; set; }

        [Required]
        public DateTime endDate { get; set; }

        [Required]
        public decimal budget { get; set; }

        [MaxLength(500)]
        public string? notes { get; set; }         

        public DateTime createdAt { get; set; } = DateTime.UtcNow;
    }
}
