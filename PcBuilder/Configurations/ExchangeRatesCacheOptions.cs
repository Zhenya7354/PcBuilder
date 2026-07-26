namespace PcBuilder.Configurations;

public class ExchangeRatesCacheOptions
{
    public int AbsoluteExpirationInHours { get; set; } = 12; 
    public long EntrySize { get; set; } = 1;
}
