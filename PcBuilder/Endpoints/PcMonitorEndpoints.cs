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

        group.MapGet(string.Empty, async ([FromServices] IPcMonitorService service, CancellationToken cancellationToken) =>
        {
            return Results.Ok(await service.GetAllMonitorsAsync(cancellationToken));
        });

        group.MapGet("/{id}", async ([FromServices] IPcMonitorService service, int id, CancellationToken cancellationToken) =>
        {
            try
            {
                return Results.Ok(await service.GetMonitorByIdAsync(id, cancellationToken));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost(string.Empty, async ([FromServices] IPcMonitorService service, [FromBody] PcMonitorCreateRequest dto, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Monitor data is required");
            try
            {
                return Results.Ok(await service.AddMonitorAsync(dto, cancellationToken));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id}", async ([FromServices] IPcMonitorService service, [FromBody] PcMonitorUpdateRequest dto, int id, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Monitor data is required");
            try
            {
                return Results.Ok(await service.UpdateMonitorAsync(id, dto, cancellationToken));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] IPcMonitorService service, int id, CancellationToken cancellationToken) =>
        {
            try
            {
                await service.DeleteMonitorAsync(id, cancellationToken);
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
