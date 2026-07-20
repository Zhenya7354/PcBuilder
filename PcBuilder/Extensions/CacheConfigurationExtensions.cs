using PcBuilder.Configurations;

namespace PcBuilder.Extensions;

public static class CacheConfigurationExtensions
{
    public static WebApplicationBuilder AddAppCacheConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services
            .Configure<CacheOptions>(builder.Configuration.GetSection("ComponentCatalogCache"));

        return builder;
    }
}
