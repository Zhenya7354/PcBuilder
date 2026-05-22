
using PcBuilder.Entities;

public class AdminEntity
{
    public int Id { get; set; }
    public string? UserId { get; set; } = string.Empty;
    public UserEntity? User { get; set; }
}