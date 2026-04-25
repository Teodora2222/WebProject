using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripService.Domain.Models
{
    public class Destination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column("travelPlanId")]
        public int travelId { get; set; }

        [Required]
        [MaxLength(200)]
        public string name { get; set; } = null!;

        [MaxLength(300)]
        public string? location { get; set; }

        [Column("arrivalDate")]
        public DateTime? startDate { get; set; }

        [Column("departureDate")]
        public DateTime? endDate { get; set; }

        [MaxLength(1000)]
        public string? description { get; set; }

        [MaxLength(1000)]
        public string? note { get; set; } 
    }
}