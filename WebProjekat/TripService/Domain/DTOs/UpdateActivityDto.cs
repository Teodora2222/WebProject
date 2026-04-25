using TripService.Domain.Enum;

namespace TripService.Domain.DTOs
{
    public class UpdateActivityDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public TimeOnly? Time { get; set; }

        public string? Location { get; set; }

        public DateTime? Date { get; set; }

        public Status? status { get; set; }

        public decimal? EstimatedCost { get; set; }
    }
}
