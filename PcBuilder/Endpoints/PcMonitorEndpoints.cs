using Microsoft.AspNetCore.Mvc;
using PcBuilder.Models;
using PcBuilder.Services;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Endpoints;

public static class PcMonitorEndpoints
{
    public static WebApplication MapPcMonitorEndpoints(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("/pc-monitors");

        group.MapGet("", async ([FromServices] IPcMonitorService service) => Results.Ok(await service.GetAllMonitorsAsync()));

        group.MapGet("/{id}", async ([FromServices] IPcMonitorService service, int id) =>
        {
            try
            {
                return Results.Ok(await service.GetMonitorByIdAsync(id));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost("", async ([FromServices] IPcMonitorService service, [FromBody] PcMonitorCreate dto) =>
        {
            if (dto is null) return Results.BadRequest("Monitor data is required");
            try
            {
                return Results.Ok(await service.AddMonitorAsync(dto));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id}", async ([FromServices] IPcMonitorService service, [FromBody] PcMonitorUpdate dto, int id) =>
        {
            if (dto is null) return Results.BadRequest("Monitor data is required");
            try
            {
                return Results.Ok(await service.UpdateMonitorAsync(id, dto));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] IPcMonitorService service, int id) =>
        {
            try
            {
                await service.DeleteMonitorAsync(id);
                return Results.Ok($"Monitor with id {id} deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        return webApplication;
    }
}
