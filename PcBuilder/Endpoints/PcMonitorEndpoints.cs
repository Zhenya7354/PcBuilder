using Microsoft.AspNetCore.Mvc;
using PcBuilder.Models;
using PcBuilder.Services;

namespace PcBuilder.Endpoints;

public static class PcMonitorEndpoints
{
    public static WebApplication MapPcMonitorEndpoints(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("/monitor");

        group.MapGet("/all", async ([FromServices] PcMonitorService service) => Results.Ok(await service.GetAllMonitorsAsync()));

        group.MapGet("/{id}", async ([FromServices] PcMonitorService service, int id) =>
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

        group.MapPost("/add", async ([FromServices] PcMonitorService service, [FromBody] PcMonitorCreate dto) =>
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

        group.MapPut("/update/{id}", async ([FromServices] PcMonitorService service, [FromBody] PcMonitorUpdate dto, int id) =>
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

        group.MapDelete("/delete/{id}", async ([FromServices] PcMonitorService service, int id) =>
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
