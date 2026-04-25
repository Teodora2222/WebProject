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

        public int travelPlanId { get; set; }

        [Required]
        [MaxLength(200)]
        public string name { get; set; } = null!;

        [Column("activityDate")]
        public DateTime date { get; set; }

        [Column("activityTime")]
        public string? time { get; set; }

        [MaxLength(300)]
        public string? location { get; set; }

        [MaxLength(1000)]
        public string? description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? estimatedCost { get; set; }

        [MaxLength(20)]
        public string status { get; set; } = "PLANNED";
        // public Status status { get; set; } = Status.PLANNED;
    }
}