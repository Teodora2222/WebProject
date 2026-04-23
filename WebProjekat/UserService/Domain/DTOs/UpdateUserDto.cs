using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.DTOs
{
    public class UpdateUserDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string role { get; set; }
    }
}
