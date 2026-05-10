using Microsoft.AspNetCore.Mvc;
using PcBuilder.Dtos;
using PcBuilder.Services;

namespace PcBuilder.Endpoints
{
    public static class MotherboardEndpoints
    {
        public static WebApplication MapMotherboardEndpoints(this WebApplication webApplication)
        {
            var group = webApplication.MapGroup("/motherboard");

            group.MapGet("/all", async ([FromServices] MotherboardService service) => Results.Ok(await service.GetAllMotherboardsAsync()));

            group.MapGet("/{id}", async ([FromServices] MotherboardService service, int id) =>
            {
                try
                {
                    return Results.Ok(await service.GetMotherboardByIdAsync(id));
                }
                catch (ArgumentException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            group.MapPost("/add", async ([FromServices] MotherboardService service, [FromBody] MotherboardCreateDto dto) =>
            {
                if (dto is null) return Results.BadRequest("Motherboard data is required");
                try
                {
                    return Results.Ok(await service.AddMotherboardAsync(dto));
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPut("/update/{id}", async ([FromServices] MotherboardService service, [FromBody] MotherboardUpdateDto dto, int id) =>
            {
                if (dto is null) return Results.BadRequest("Motherboard data is required");
                try
                {
                    return Results.Ok(await service.UpdateMotherboardAsync(id, dto));
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapDelete("/delete/{id}", async ([FromServices] MotherboardService service, int id) =>
            {
                try
                {
                    await service.DeleteMotherboardAsync(id);
                    return Results.Ok($"Motherboard with id {id} deleted successfully");
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
