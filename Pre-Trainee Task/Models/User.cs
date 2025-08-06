using System.ComponentModel.DataAnnotations;

namespace Pre_Trainee_Task.Models;

public class User
{
    public Guid Id { get; init; }

    [EmailAddress] 
    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}
