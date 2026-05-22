namespace PcBuilder.Models;

public record AuthResponse(
   string Token,
   string Email,
   IList<string> Roles,
   DateTime ExpiresAt
   );
