using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Models
{
    [Table("Users")]
    public class User
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        public string firstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string lastName { get; set; }

        [Required]
        [MaxLength(200)]
        public string email { get; set; }

        [Required]
        public string passwordHash { get; set; }  

        [MaxLength(20)]
        public string role { get; set; } = "user";

        public DateTime createdAt { get; set; } = DateTime.UtcNow;
    }
}
