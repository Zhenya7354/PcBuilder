namespace PcBuilder.Entities;

public class RegularUserEntity
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string PrefferedCurrency { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public int BuildsCreated { get; set; }
    public UserEntity? User { get; set; }
}