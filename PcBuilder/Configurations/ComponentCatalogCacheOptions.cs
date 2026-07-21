namespace PcBuilder.Configurations;

public class CacheOptions
{
    public int AbsoluteExpirationInMinutes { get; set; } = 30;
    public int SlidingExpirationInMinutes { get; set; } = 10;
    public int SizeLimit { get; set; } = 1;
}
