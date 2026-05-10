using Microsoft.AspNetCore.Mvc;
using PcBuilder.Dtos;
using PcBuilder.Services;

namespace PcBuilder.Endpoints
{
    public static class HardDriveEndpoints
    {
        public static WebApplication MapHardDriveEndpoints(this WebApplication webApplication)
        {
            var group = webApplication.MapGroup("/hard-drive");

            group.MapGet("/all", async ([FromServices] HardDriveService service) => Results.Ok(await service.GetAllHardDrivesAsync()));

            group.MapGet("/{id}", async ([FromServices] HardDriveService service, int id) =>
            {
                try
                {
                    return Results.Ok(await service.GetHardDriveByIdAsync(id));
                }
                catch (ArgumentException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            group.MapPost("/add", async ([FromServices] HardDriveService service, [FromBody] HardDriveCreateDto dto) =>
            {
                if (dto is null) return Results.BadRequest("Hard drive data is required");
                try
                {
                    return Results.Ok(await service.AddHardDriveAsync(dto));
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPut("/update/{id}", async ([FromServices] HardDriveService service, [FromBody] HardDriveUpdateDto dto, int id) =>
            {
                if (dto is null) return Results.BadRequest("Hard drive data is required");
                try
                {
                    return Results.Ok(await service.UpdateHardDriveAsync(id, dto));
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapDelete("/delete/{id}", async ([FromServices] HardDriveService service, int id) =>
            {
                try
                {
                    await service.DeleteHardDriveAsync(id);
                    return Results.Ok($"Hard drive with id {id} deleted successfully");
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
