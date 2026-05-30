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

        group.MapGet(string.Empty, async ([FromServices] IHardDriveService service) =>
        {
            return Results.Ok(await service.GetAllHardDrivesAsync());
        });

        group.MapGet("/{id}", async ([FromServices] IHardDriveService service, int id) =>
        {
            try
            {
                return Results.Ok(await service.GetHardDriveByIdAsync(id));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost(string.Empty, async ([FromServices] IHardDriveService service, [FromBody] HardDriveCreate dto) =>
        {
            if (dto is null) return Results.BadRequest("Hard drive data is required");
            try
            {
                return Results.Ok(await service.AddHardDriveAsync(dto));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id}", async ([FromServices] IHardDriveService service, [FromBody] HardDriveUpdate dto, int id) =>
        {
            if (dto is null) return Results.BadRequest("Hard drive data is required");
            try
            {
                return Results.Ok(await service.UpdateHardDriveAsync(id, dto));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] IHardDriveService service, int id) =>
        {
            try
            {
                await service.DeleteHardDriveAsync(id);
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
