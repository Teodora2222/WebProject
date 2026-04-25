using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TripService.Domain.Enum;

namespace TripService.Domain.Models
{
    public class Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int travelPlanId { get; set; }  

        [Required]
        [MaxLength(200)]
        public string name { get; set; } = null!;

        public DateTime date { get; set; }

        public TimeOnly? time { get; set; }  

        [MaxLength(200)]
        public string? location { get; set; }

        [MaxLength(200)]
        public string? description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? estimatedCost { get; set; }

        public Status status { get; set; } = Status.PLANNED;
    }
}

