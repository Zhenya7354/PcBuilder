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

        group.MapGet(string.Empty, async ([FromServices] IPsuService service, CancellationToken cancellationToken) =>
        {
            return Results.Ok(await service.GetAllPsusAsync(cancellationToken));
        });

        group.MapGet("/{id}", async ([FromServices] IPsuService service, int id, CancellationToken cancellationToken) =>
        {
            try
            {
                return Results.Ok(await service.GetPsuByIdAsync(id, cancellationToken));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost(string.Empty, async ([FromServices] IPsuService service, [FromBody] PsuCreateRequest dto, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Psu data is required");
            try
            {
                return Results.Ok(await service.AddPsuAsync(dto, cancellationToken));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id}", async ([FromServices] IPsuService service, [FromBody] PsuUpdateRequest dto, int id, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Psu data is required");
            try
            {
                return Results.Ok(await service.UpdatePsuAsync(id, dto, cancellationToken));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] IPsuService service, int id, CancellationToken cancellationToken) =>
        {
            try
            {
                await service.DeletePsuAsync(id, cancellationToken);
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
