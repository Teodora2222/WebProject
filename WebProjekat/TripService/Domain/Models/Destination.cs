using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripService.Domain.Models
{
    public class Destination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int travelId { get; set; }

        [Required]
        [MaxLength(200)]
        public string name { get; set; } = null!;

        
        [MaxLength(200)]
        public string? description { get; set; }

        [MaxLength(200)]
        public string? location { get ; set; }

        [Required]
        public DateTime? startDate { get; set; }

        [Required]
        public DateTime? endDate { get; set; }

        
        [MaxLength(200)]
        public string? note { get; set; }
    }
}
