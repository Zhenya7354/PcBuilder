using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Dtos
{
    public record RegisterDto(
        [Required, EmailAddress] string Email,
        [Required, MinLength(8)] string Password
        );
}
