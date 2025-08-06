using System.ComponentModel.DataAnnotations;

namespace Pre_Trainee_Task.Models;

public class User
{
    [Key]
    [Required]
    public Guid Id { get; init; }
    [Required]
    [MaxLength(50)]
    [EmailAddress] 
    public string Email { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string PasswordHash { get; set; } = string.Empty;
    [Required]
    public UserRole Role { get; set; }
}
