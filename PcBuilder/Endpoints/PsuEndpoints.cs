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
                return Results.Ok(await service.GetPsuByIdAsync(id, cancellationToken));
        });

        group.MapPost(string.Empty, async ([FromServices] IPsuService service, [FromBody] PsuCreateRequest dto, CancellationToken cancellationToken) =>
        {
                return Results.Ok(await service.AddPsuAsync(dto, cancellationToken));
        });

        group.MapPut("/{id}", async ([FromServices] IPsuService service, [FromBody] PsuUpdateRequest dto, int id, CancellationToken cancellationToken) =>
        {
                return Results.Ok(await service.UpdatePsuAsync(id, dto, cancellationToken));
        });

        group.MapDelete("/{id}", async ([FromServices] IPsuService service, int id, CancellationToken cancellationToken) =>
        {
                await service.DeletePsuAsync(id, cancellationToken);
                return Results.Ok($"Psu with id {id} deleted successfully");
        });

        return webApplication;
    }
}
