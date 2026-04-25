using TripService.Domain.Enum;

namespace TripService.Domain.DTOs
{
    public class CreateActivityDto
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? Time { get; set; }

        public string? Location { get; set; }

        public DateTime Date {  get; set; }

        public string status { get; set; } = "PLANNED";
        public decimal? EstimatedCost { get; set; }
    }
}
