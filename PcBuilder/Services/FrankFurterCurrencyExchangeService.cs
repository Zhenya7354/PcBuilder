using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PcBuilder.Configurations;
using PcBuilder.Enums;
using PcBuilder.Models;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class FrankFurterCurrencyExchangeService
    (HttpClient httpClient,
    IMemoryCache cache,
    IOptions<ExchangeRatesCacheOptions> exchangeRatesCacheOptions): ICurrencyExchangeService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IMemoryCache _cache = cache;
    private readonly ExchangeRatesCacheOptions _exchangeRatesCacheOptions = exchangeRatesCacheOptions.Value;
    public async Task<decimal> ConvertAsync(decimal amount, Currency from, Currency to, CancellationToken cancellationToken)
    {
        if(from == to)
        {
            return amount;
        }
        var rate = await GetRateAsync(from, to, cancellationToken);
        return amount * rate;
    }
    private async Task<decimal> GetRateAsync(
        Currency from,
        Currency to,
        CancellationToken cancellationToken)
    {
        var cacheKey = ExchangeRateCacheKey.GetKey(from, to);
        var rate = await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(_exchangeRatesCacheOptions.AbsoluteExpirationInHours);
            entry.Size = _exchangeRatesCacheOptions.EntrySize;

            var response = await _httpClient.GetFromJsonAsync<FrankfurterRateResponse>($"/v2/rate/{from}/{to}", cancellationToken);
            return response?.Rate ?? throw new InvalidOperationException($"Rate not found for {from} to {to}");
        });
        return rate;
    }
}
