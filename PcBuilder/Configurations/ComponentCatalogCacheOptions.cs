namespace PcBuilder.Configurations;

public class CacheOptions
{
    public int CacheAbsoluteExpirationInMinutes { get; set; } = 30;
    public int CacheSlidingExpirationInMinutes { get; set; } = 10;
    public int CacheSize { get; set; } = 1;
}
