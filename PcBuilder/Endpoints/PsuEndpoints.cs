using Microsoft.AspNetCore.Mvc;
using PcBuilder.Models;
using PcBuilder.Services;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Endpoints;

public static class PsuEndpoints
{
    public static WebApplication MapPsuEndpoints(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("/psus");

        group.MapGet(string.Empty, async ([FromServices] IPsuService service) => Results.Ok(await service.GetAllPsusAsync()));

        group.MapGet("/{id}", async ([FromServices] IPsuService service, int id) =>
        {
            try
            {
                return Results.Ok(await service.GetPsuByIdAsync(id));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost(string.Empty, async ([FromServices] IPsuService service, [FromBody] PsuCreateRequest dto) =>
        {
            if (dto is null) return Results.BadRequest("Psu data is required");
            try
            {
                return Results.Ok(await service.AddPsuAsync(dto));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id}", async ([FromServices] IPsuService service, [FromBody] PsuUpdateRequest dto, int id) =>
        {
            if (dto is null) return Results.BadRequest("Psu data is required");
            try
            {
                return Results.Ok(await service.UpdatePsuAsync(id, dto));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] IPsuService service, int id) =>
        {
            try
            {
                await service.DeletePsuAsync(id);
                return Results.Ok($"Psu with id {id} deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        return webApplication;
    }
}
