using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models
{
    public record RegisterDto(
        [Required, EmailAddress] string Email,
        [Required, MinLength(8)] string Password
        );

    public record LoginDto(
        [Required, EmailAddress] string Email,
        [Required, MinLength(8)] string Password 
        );

    public record AuthResponseDto(
        string Token,
        string Email,
        IList<string> Roles,
        DateTime ExpiresAt
        );
}
