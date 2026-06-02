using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Endpoints;
using PcBuilder.Extentions;
using PcBuilder.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.AddAppServices();
builder.AddIdentityAndJwt();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();
    await seeder.SeedRolesAsync(scope.ServiceProvider);
    await seeder.SeedDataAsync();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapAuthEndpoints();
app.MapCpuEndpoints();
app.MapGpuEndpoints();
app.MapCpuCoolerEndpoints();
app.MapMotherboardEndpoints();
app.MapRamEndpoints();
app.MapPsuEndpoints();
app.MapPcCaseEndpoints();
app.MapPcMonitorEndpoints();
app.MapHardDriveEndpoints();
app.MapBrandEndpoints();
app.MapBuildEndpoints();

app.Run();


