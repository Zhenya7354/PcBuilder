using Microsoft.AspNetCore.Mvc;
using PcBuilder.Dtos;
using PcBuilder.Services;

namespace PcBuilder.Endpoints
{
    public static class CpuCoolerEndpoints
    {
        public static WebApplication MapCpuCoolerEndpoints(this WebApplication webApplication)
        {
            var group = webApplication.MapGroup("/cpu-cooler");

            group.MapGet("/all", async ([FromServices] CpuCoolerService service) =>
            {
                var cpuCoolers = await service.GetAllCpuCoolersAsync();
                return Results.Ok(cpuCoolers);
            });

            group.MapGet("/{id}", async ([FromServices] CpuCoolerService service, int id) =>
            {
                try
                {
                    var cpuCooler = await service.GetCpuCoolerByIdAsync(id);
                    return Results.Ok(cpuCooler);
                }
                catch (ArgumentException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            group.MapPost("/add", async ([FromServices] CpuCoolerService service, [FromBody] CpuCoolerCreateDto dto) =>
            {
                if (dto is null) return Results.BadRequest("Cpu cooler data is required");
                try
                {
                    var cpuCooler = await service.AddCpuCoolerAsync(dto);
                    return Results.Ok(cpuCooler);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPut("/update/{id}", async ([FromServices] CpuCoolerService service, [FromBody] CpuCoolerUpdateDto dto, int id) =>
            {
                if (dto is null) return Results.BadRequest("Cpu cooler data is required");
                try
                {
                    var cpuCooler = await service.UpdateCpuCoolerAsync(id, dto);
                    return Results.Ok(cpuCooler);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapDelete("/delete/{id}", async ([FromServices] CpuCoolerService service, int id) =>
            {
                try
                {
                    await service.DeleteCpuCoolerAsync(id);
                    return Results.Ok($"Cpu cooler with id {id} deleted successfully");
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
