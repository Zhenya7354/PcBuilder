using Microsoft.AspNetCore.Mvc;
using PcBuilder.Dtos;
using PcBuilder.Services;

namespace PcBuilder.Endpoints
{
    public static class PcCaseEndpoints
    {
        public static WebApplication MapPcCaseEndpoints(this WebApplication webApplication)
        {
            var group = webApplication.MapGroup("/case");

            group.MapGet("/all", async ([FromServices] PcCaseService service) => Results.Ok(await service.GetAllCasesAsync()));

            group.MapGet("/{id}", async ([FromServices] PcCaseService service, int id) =>
            {
                try
                {
                    return Results.Ok(await service.GetCaseByIdAsync(id));
                }
                catch (ArgumentException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            group.MapPost("/add", async ([FromServices] PcCaseService service, [FromBody] PcCaseCreateDto dto) =>
            {
                if (dto is null) return Results.BadRequest("Case data is required");
                try
                {
                    return Results.Ok(await service.AddCaseAsync(dto));
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPut("/update/{id}", async ([FromServices] PcCaseService service, [FromBody] PcCaseUpdateDto dto, int id) =>
            {
                if (dto is null) return Results.BadRequest("Case data is required");
                try
                {
                    return Results.Ok(await service.UpdateCaseAsync(id, dto));
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapDelete("/delete/{id}", async ([FromServices] PcCaseService service, int id) =>
            {
                try
                {
                    await service.DeleteCaseAsync(id);
                    return Results.Ok($"Case with id {id} deleted successfully");
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            return webApplication;
        }
    }
}
