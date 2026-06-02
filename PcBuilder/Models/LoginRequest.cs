using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

public record LoginRequest(
    [Required, EmailAddress] string Email,
    [Required, MinLength(8)] string Password
    );
