using PcBuilder.Enums;

namespace PcBuilder.Models;

public static class ExchangeRateCacheKey
{
    public static string GetKey(Currency from, Currency to)
    {
        return $"ExchangeRate_{from}_{to}";
    }
}
