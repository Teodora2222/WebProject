namespace TripService.Domain.DTOs
{

    public class ChecklistItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsCompleted { get; set; }
    }
}
