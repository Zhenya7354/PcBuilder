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

        group.MapGet(string.Empty, async ([FromServices] IMotherboardService service, CancellationToken cancellationToken) =>
        {
            return Results.Ok(await service.GetAllMotherboardsAsync(cancellationToken));
        });

        group.MapGet("/{id}", async ([FromServices] IMotherboardService service, int id, CancellationToken cancellationToken) =>
        {
                return Results.Ok(await service.GetMotherboardByIdAsync(id, cancellationToken));
        });

        group.MapPost(string.Empty, async ([FromServices] IMotherboardService service, [FromBody] MotherboardCreateRequest dto, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Motherboard data is required");
                return Results.Ok(await service.AddMotherboardAsync(dto, cancellationToken));
        });

        group.MapPut("/{id}", async ([FromServices] IMotherboardService service, [FromBody] MotherboardUpdateRequest dto, int id, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Motherboard data is required");
                return Results.Ok(await service.UpdateMotherboardAsync(id, dto, cancellationToken));
        });

        group.MapDelete("/{id}", async ([FromServices] IMotherboardService service, int id, CancellationToken cancellationToken) =>
        {
                await service.DeleteMotherboardAsync(id, cancellationToken);
                return Results.Ok($"Motherboard with id {id} deleted successfully");
        });

        return webApplication;
    }
}
