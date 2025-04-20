using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public String FullName { get; set; } = String.Empty;
        [Required, EmailAddress]
        public String Email { get; set; } = String.Empty;
        [Required]
        public String PasswordHash { get; set; } = String.Empty;
        public String Role { get; set; }
        public ICollection<TaskItem>? Tasks { get; set; }
    }
}
