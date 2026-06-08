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
            try
            {
                return Results.Ok(await service.GetCaseByIdAsync(id, cancellationToken));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost(string.Empty, async ([FromServices] IPcCaseService service, [FromBody] PcCaseCreateRequest dto, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Case data is required");
            try
            {
                return Results.Ok(await service.AddCaseAsync(dto, cancellationToken));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id}", async ([FromServices] IPcCaseService service, [FromBody] PcCaseUpdateRequest dto, int id, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Case data is required");
            try
            {
                return Results.Ok(await service.UpdateCaseAsync(id, dto, cancellationToken));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] IPcCaseService service, int id, CancellationToken cancellationToken) =>
        {
            try
            {
                await service.DeleteCaseAsync(id, cancellationToken);
                return Results.Ok($"Case with id {id} deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        return webApplication;
    }
}
