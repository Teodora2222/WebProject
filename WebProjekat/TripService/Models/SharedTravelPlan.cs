using System.ComponentModel.DataAnnotations.Schema;

namespace TripService.Domain.Models
{
    [Table("SharedPlans")]
    public class SharedTravelPlan
    {
        public int Id { get; set; }
        public int TravelPlanId { get; set; }
        public string Token { get; set; } = Guid.NewGuid().ToString();

        [Column("accessType")]
        public string Permission { get; set; } = "VIEW";

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }
    }
}
