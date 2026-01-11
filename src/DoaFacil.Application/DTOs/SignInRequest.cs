using System.ComponentModel.DataAnnotations;

namespace DoaFacil.Application.DTOs;

public class SignInRequest
{
    [Required]
    [EmailAddress]
    [StringLength(256)]
    public string Email { get; init; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; init; } = string.Empty;

    public bool RememberMe { get; init; }
}
