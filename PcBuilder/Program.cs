using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
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
    var db = scope.ServiceProvider.GetRequiredService<PcDbContext>();
    await db.Database.MigrateAsync();
    await DbSeeder.SeedRolesAsync(scope.ServiceProvider);
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


app.Run();


