using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Pre_Trainee_Task.DTOs;

public class UserDto
{
    [Required] 
    [MaxLength(50)] 
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    [DefaultValue("admin@admin.com")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    [MaxLength(50)]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    [DefaultValue("password")]
    public string Password { get; set; } = string.Empty;
}
