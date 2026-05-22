namespace PcBuilder.Models;

public class RegularUser
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string PrefferedCurrency { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public int BuildsCreated { get; set; }
    public User? User { get; set; }
}
