using Microsoft.AspNetCore.Mvc;
using PcBuilder.Models;
using PcBuilder.Services;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Endpoints;

public static class HardDriveEndpoints
{
    public static WebApplication MapHardDriveEndpoints(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("/hard-drives");

        group.MapGet(string.Empty, async ([FromServices] IHardDriveService service, CancellationToken cancellationToken) =>
        {
            return Results.Ok(await service.GetAllHardDrivesAsync(cancellationToken));
        });

        group.MapGet("/{id}", async ([FromServices] IHardDriveService service, int id, CancellationToken cancellationToken) =>
        {
            try
            {
                return Results.Ok(await service.GetHardDriveByIdAsync(id, cancellationToken));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost(string.Empty, async ([FromServices] IHardDriveService service, [FromBody] HardDriveCreateRequest dto, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Hard drive data is required");
            try
            {
                return Results.Ok(await service.AddHardDriveAsync(dto, cancellationToken));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id}", async ([FromServices] IHardDriveService service, [FromBody] HardDriveUpdateRequest dto, int id, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Hard drive data is required");
            try
            {
                return Results.Ok(await service.UpdateHardDriveAsync(id, dto, cancellationToken));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] IHardDriveService service, int id, CancellationToken cancellationToken) =>
        {
            try
            {
                await service.DeleteHardDriveAsync(id, cancellationToken);
                return Results.Ok($"Hard drive with id {id} deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        return webApplication;
    }
}
