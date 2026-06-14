using Microsoft.AspNetCore.Mvc;
using PcBuilder.Models;
using PcBuilder.Services;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Endpoints;

public static class PcCaseEndpoints
{
    public static WebApplication MapPcCaseEndpoints(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("/pc-cases");

        group.MapGet(string.Empty, async ([FromServices] IPcCaseService service, CancellationToken cancellationToken) =>
        {
            return Results.Ok(await service.GetAllCasesAsync(cancellationToken));
        });

        group.MapGet("/{id}", async ([FromServices] IPcCaseService service, int id, CancellationToken cancellationToken) =>
        {
                return Results.Ok(await service.GetCaseByIdAsync(id, cancellationToken));
        });

        group.MapPost(string.Empty, async ([FromServices] IPcCaseService service, [FromBody] PcCaseCreateRequest dto, CancellationToken cancellationToken) =>
        {
                return Results.Ok(await service.AddCaseAsync(dto, cancellationToken));
        });

        group.MapPut("/{id}", async ([FromServices] IPcCaseService service, [FromBody] PcCaseUpdateRequest dto, int id, CancellationToken cancellationToken) =>
        {
                return Results.Ok(await service.UpdateCaseAsync(id, dto, cancellationToken));
        });

        group.MapDelete("/{id}", async ([FromServices] IPcCaseService service, int id, CancellationToken cancellationToken) =>
        {
                await service.DeleteCaseAsync(id, cancellationToken);
                return Results.Ok($"Case with id {id} deleted successfully");
        });

        return webApplication;
    }
}
