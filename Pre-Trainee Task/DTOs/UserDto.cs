using System.ComponentModel.DataAnnotations;

namespace Pre_Trainee_Task.DTOs;

public class UserDto
{
    [Required] [MaxLength(50)] public string Email { get; set; } = string.Empty;

    [Required]
    // [MinLength(8)] Disabled for testing purposes
    [MaxLength(50)]
    public string Password { get; set; } = string.Empty;
}