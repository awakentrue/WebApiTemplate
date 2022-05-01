using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Requests;

public class LoginRequest
{
    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}