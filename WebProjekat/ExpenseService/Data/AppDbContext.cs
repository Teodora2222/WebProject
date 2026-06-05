using ExpenseService.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                .Property(e => e.amount)
                .HasPrecision(18, 2);
        }
    }
}
