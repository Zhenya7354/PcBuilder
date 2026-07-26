using PcBuilder.Configurations;

namespace PcBuilder.Extensions;

public static class CacheConfigurationExtensions
{
    public static WebApplicationBuilder AddAppCacheConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services
            .Configure<ComponentCatalogCacheOptions>(builder.Configuration.GetSection("ComponentCatalogCache"));
        builder.Services
            .Configure<ExchangeRatesCacheOptions>(builder.Configuration.GetSection("ExchangeRatesCache"));

        builder.Services.AddMemoryCache(options =>
            options.SizeLimit = builder.Configuration.GetValue<int>("ComponentCatalogCache:SizeLimit"));

        return builder;
    }
}
