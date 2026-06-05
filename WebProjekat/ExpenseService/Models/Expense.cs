using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseService.Models
{
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int travelPlanId { get; set; }

        [Required]
        [MaxLength(200)]
        public string name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string category { get; set; } = null!;

        [Column(TypeName = "decimal(10,2)")]
        public decimal amount { get; set; }


        [Column("expenseDate")]
        public DateTime date { get; set; }

        [MaxLength(500)]
        public string? description { get; set; }
    }
}
