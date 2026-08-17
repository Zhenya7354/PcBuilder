using PcBuilder.Data.Seeding;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Data.Seeding.Seeders;

namespace PcBuilder.Extentions;

public static class SeedExtensions
{
    public static WebApplicationBuilder AddSeeders(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IDataSeeder, BrandSeeder>();
        builder.Services.AddScoped<IDataSeeder, CpuCoolerSeeder>();
        builder.Services.AddScoped<IDataSeeder, CpuSeeder>();
        builder.Services.AddScoped<IDataSeeder, GpuSeeder>();
        builder.Services.AddScoped<IDataSeeder, HardDriveSeeder>();
        builder.Services.AddScoped<IDataSeeder, MotherboardSeeder>();
        builder.Services.AddScoped<IDataSeeder, PcCaseSeeder>();
        builder.Services.AddScoped<IDataSeeder, PcMonitorSeeder>();
        builder.Services.AddScoped<IDataSeeder, PsuSeeder>();
        builder.Services.AddScoped<IDataSeeder, RamSeeder>();
        builder.Services.AddScoped<IIdentityRolesSeeder, IdentityRolesSeeder>();
        builder.Services.AddScoped<IDbSeeder, DbSeeder>();
        return builder;
    }
    public static WebApplication AddDataSeeding(this WebApplication app)
    {
        if (!app.Environment.IsEnvironment("IntegrationTests"))
        {
            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();
            seeder.SeedDataAsync(scope.ServiceProvider);
        }
        return app;
    }
}
