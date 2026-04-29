using ExpenseService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Expense> Expenses { get; set; }
    }
}
