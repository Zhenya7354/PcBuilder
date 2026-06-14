using Microsoft.AspNetCore.Mvc;
using PcBuilder.Models;
using PcBuilder.Services;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Endpoints;

public static class RamEndpoints
{
    public static WebApplication MapRamEndpoints(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("/rams");

        group.MapGet(string.Empty, async ([FromServices] IRamService service, CancellationToken cancellationToken) =>
        {
            return Results.Ok(await service.GetAllRamAsync(cancellationToken));
        });

        group.MapGet("/{id}", async ([FromServices] IRamService service, int id, CancellationToken cancellationToken) =>
        {
                return Results.Ok(await service.GetRamByIdAsync(id, cancellationToken));
        });

        group.MapPost(string.Empty, async ([FromServices] IRamService service, [FromBody] RamCreateRequest dto, CancellationToken cancellationToken) =>
        {
                return Results.Ok(await service.AddRamAsync(dto, cancellationToken));
        });

        group.MapPut("/{id}", async ([FromServices] IRamService service, [FromBody] RamUpdateRequest dto, int id, CancellationToken cancellationToken) =>
        {
                return Results.Ok(await service.UpdateRamAsync(id, dto, cancellationToken));
        });

        group.MapDelete("/{id}", async ([FromServices] IRamService service, int id, CancellationToken cancellationToken) =>
        {
                await service.DeleteRamAsync(id, cancellationToken);
                return Results.Ok($"Ram with id {id} deleted successfully");
        });

        return webApplication;
    }
}
