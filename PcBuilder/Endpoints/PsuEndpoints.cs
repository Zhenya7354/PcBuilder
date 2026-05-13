using Microsoft.AspNetCore.Mvc;
using PcBuilder.Dtos;
using PcBuilder.Services;

namespace PcBuilder.Endpoints
{
    public static class PsuEndpoints
    {
        public static WebApplication MapPsuEndpoints(this WebApplication webApplication)
        {
            var group = webApplication.MapGroup("/psu");

            group.MapGet("/all", async ([FromServices] PsuService service) => Results.Ok(await service.GetAllPsusAsync()));

            group.MapGet("/{id}", async ([FromServices] PsuService service, int id) =>
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

            group.MapPost("/add", async ([FromServices] PsuService service, [FromBody] PsuCreateDto dto) =>
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

            group.MapPut("/update/{id}", async ([FromServices] PsuService service, [FromBody] PsuUpdateDto dto, int id) =>
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

            group.MapDelete("/delete/{id}", async ([FromServices] PsuService service, int id) =>
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
}
