using Microsoft.EntityFrameworkCore;
using TripService.Domain.Models;

namespace TripService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<TravelPlan> TravelPlans { get; set; }
        public DbSet<Destination> Destinations { get; set; }

        public DbSet<ChecklistItem> ChecklistItems { get; set; }

        public DbSet<Activity> Activities { get; set; }
    }
}
