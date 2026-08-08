using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using PcBuilder.Data;
using PcBuilder.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PcBuilder.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IAsyncLifetime
{
    private IServiceScope? _scope;
    protected PcDbContext? DbContext;
    protected HttpClient HttpClient;
    protected TestDataSeeder? DataSeeder;
    protected ICpuService? CpuService;
    private readonly IntegrationTestWebAppFactory _factory;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _factory = factory;
    }

    public async Task InitializeAsync()
    {
        _scope = _factory.Services.CreateScope();
        DbContext = _scope.ServiceProvider.GetRequiredService<PcDbContext>();
        DataSeeder = new TestDataSeeder(DbContext);
        CpuService = _scope.ServiceProvider.GetRequiredService<ICpuService>();
        HttpClient = _factory.CreateClient();

        if (DbContext.Database.GetPendingMigrations().Any())
        {
            await DbContext.Database.MigrateAsync();
        }

        if (DataSeeder is not null)
        {
            await DataSeeder.SeedAsync();
        }
    }

    public Task DisposeAsync()
    {
        HttpClient?.Dispose();
        _scope?.Dispose();
        return Task.CompletedTask;
    }
}
