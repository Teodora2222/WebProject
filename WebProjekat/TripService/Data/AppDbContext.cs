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

        public DbSet<SharedTravelPlan> SharedTravelPlans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .HasOne<TravelPlan>()
                .WithMany()
                .HasForeignKey(a => a.travelPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Destination>()
                .HasOne<TravelPlan>()
                .WithMany()
                .HasForeignKey(d => d.travelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChecklistItem>()
                .HasOne<TravelPlan>()
                .WithMany()
                .HasForeignKey(c => c.TravelPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SharedTravelPlan>()
                .HasOne<TravelPlan>()
                .WithMany()
                .HasForeignKey(s => s.TravelPlanId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TravelPlan>()
                .Property(t => t.budget)
                .HasPrecision(18, 2);
        }
    }
}
