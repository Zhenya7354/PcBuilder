namespace PcBuilder.Dtos
{
    public record AuthResponseDto(
       string Token,
       string Email,
       IList<string> Roles,
       DateTime ExpiresAt
       );
}
