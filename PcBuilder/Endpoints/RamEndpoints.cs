using Microsoft.AspNetCore.Mvc;
using PcBuilder.Models;
using PcBuilder.Services;

namespace PcBuilder.Endpoints;

public static class RamEndpoints
{
    public static WebApplication MapRamEndpoints(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("/rams");

        group.MapGet("", async ([FromServices] RamService service) => Results.Ok(await service.GetAllRamAsync()));

        group.MapGet("/{id}", async ([FromServices] RamService service, int id) =>
        {
            try
            {
                return Results.Ok(await service.GetRamByIdAsync(id));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost("", async ([FromServices] RamService service, [FromBody] RamCreate dto) =>
        {
            if (dto is null) return Results.BadRequest("Ram data is required");
            try
            {
                return Results.Ok(await service.AddRamAsync(dto));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id}", async ([FromServices] RamService service, [FromBody] RamUpdate dto, int id) =>
        {
            if (dto is null) return Results.BadRequest("Ram data is required");
            try
            {
                return Results.Ok(await service.UpdateRamAsync(id, dto));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] RamService service, int id) =>
        {
            try
            {
                await service.DeleteRamAsync(id);
                return Results.Ok($"Ram with id {id} deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        return webApplication;
    }
}
