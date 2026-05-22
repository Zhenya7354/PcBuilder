using Microsoft.AspNetCore.Mvc;
using PcBuilder.Models;
using PcBuilder.Services;

namespace PcBuilder.Endpoints;

public static class MotherboardEndpoints
{
    public static WebApplication MapMotherboardEndpoints(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("/motherboard");

        group.MapGet("/all", async ([FromServices] MotherboardService service) => Results.Ok(await service.GetAllMotherboardsAsync()));

        group.MapGet("/{id}", async ([FromServices] MotherboardService service, int id) =>
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

        group.MapPost("/add", async ([FromServices] MotherboardService service, [FromBody] MotherboardCreate dto) =>
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

        group.MapPut("/update/{id}", async ([FromServices] MotherboardService service, [FromBody] MotherboardUpdate dto, int id) =>
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

        group.MapDelete("/delete/{id}", async ([FromServices] MotherboardService service, int id) =>
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
