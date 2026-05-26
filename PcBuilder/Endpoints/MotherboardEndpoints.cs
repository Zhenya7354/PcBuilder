using Microsoft.AspNetCore.Mvc;
using PcBuilder.Models;
using PcBuilder.Services;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Endpoints;

public static class MotherboardEndpoints
{
    public static WebApplication MapMotherboardEndpoints(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("/motherboards");

        group.MapGet("", async ([FromServices] IMotherboardService service) => Results.Ok(await service.GetAllMotherboardsAsync()));

        group.MapGet("/{id}", async ([FromServices] IMotherboardService service, int id) =>
        {
            try
            {
                return Results.Ok(await service.GetMotherboardByIdAsync(id));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost("", async ([FromServices] IMotherboardService service, [FromBody] MotherboardCreate dto) =>
        {
            if (dto is null) return Results.BadRequest("Motherboard data is required");
            try
            {
                return Results.Ok(await service.AddMotherboardAsync(dto));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id}", async ([FromServices] IMotherboardService service, [FromBody] MotherboardUpdate dto, int id) =>
        {
            if (dto is null) return Results.BadRequest("Motherboard data is required");
            try
            {
                return Results.Ok(await service.UpdateMotherboardAsync(id, dto));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] IMotherboardService service, int id) =>
        {
            try
            {
                await service.DeleteMotherboardAsync(id);
                return Results.Ok($"Motherboard with id {id} deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        return webApplication;
    }
}
