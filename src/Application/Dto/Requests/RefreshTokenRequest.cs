using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Requests;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = null!;
}