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

        group.MapGet(string.Empty, async ([FromServices] IPcCaseService service) => Results.Ok(await service.GetAllCasesAsync()));

        group.MapGet("/{id}", async ([FromServices] IPcCaseService service, int id) =>
        {
            try
            {
                return Results.Ok(await service.GetCaseByIdAsync(id));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost(string.Empty, async ([FromServices] IPcCaseService service, [FromBody] PcCaseCreateRequest dto) =>
        {
            if (dto is null) return Results.BadRequest("Case data is required");
            try
            {
                return Results.Ok(await service.AddCaseAsync(dto));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id}", async ([FromServices] IPcCaseService service, [FromBody] PcCaseUpdateRequest dto, int id) =>
        {
            if (dto is null) return Results.BadRequest("Case data is required");
            try
            {
                return Results.Ok(await service.UpdateCaseAsync(id, dto));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] IPcCaseService service, int id) =>
        {
            try
            {
                await service.DeleteCaseAsync(id);
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
