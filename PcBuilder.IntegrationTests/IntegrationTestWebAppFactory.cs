using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PcBuilder.Data;
using Testcontainers.PostgreSql;
using Microsoft.Extensions.Configuration;

namespace PcBuilder.IntegrationTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder("postgres:latest")
        .WithDatabase("pcbuilder_test_db")
        .WithUsername("postgres")
        .WithPassword("test_postgres")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTests");

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddUserSecrets<Program>(optional: true);
        });

        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Test";
                options.DefaultChallengeScheme = "Test";
            })
            .AddScheme<AuthenticationSchemeOptions, AlwaysAuthorizedAuthHandler>("Test", options => { });

            var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<PcDbContext>));

            if(descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<PcDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString());
            });
        });
    }

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}
