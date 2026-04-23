namespace TripService.Domain.DTOs
{
    public class CreateDestinationDto
    {
        public string? Name {  get; set; }

        public string? Description { get; set; }

        public string? Location { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Note { get; set; }
    }
}
