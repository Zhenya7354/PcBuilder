using Microsoft.AspNetCore.Mvc;
using PcBuilder.Models;
using PcBuilder.Services;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Endpoints;

public static class CpuCoolerEndpoints
{
    public static WebApplication MapCpuCoolerEndpoints(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("/cpu-coolers");

        group.MapGet("", async ([FromServices] ICpuCoolerService service) =>
        {
            var cpuCoolers = await service.GetAllCpuCoolersAsync();
            return Results.Ok(cpuCoolers);
        });

        group.MapGet("/{id}", async ([FromServices] ICpuCoolerService service, int id) =>
        {
            try
            {
                var cpuCooler = await service.GetCpuCoolerByIdAsync(id);
                return Results.Ok(cpuCooler);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost("", async ([FromServices] ICpuCoolerService service, [FromBody] CpuCoolerCreate dto) =>
        {
            if (dto is null) return Results.BadRequest("Cpu cooler data is required");
            try
            {
                var cpuCooler = await service.AddCpuCoolerAsync(dto);
                return Results.Ok(cpuCooler);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id}", async ([FromServices] ICpuCoolerService service, [FromBody] CpuCoolerUpdate dto, int id) =>
        {
            if (dto is null) return Results.BadRequest("Cpu cooler data is required");
            try
            {
                var cpuCooler = await service.UpdateCpuCoolerAsync(id, dto);
                return Results.Ok(cpuCooler);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] ICpuCoolerService service, int id) =>
        {
            try
            {
                await service.DeleteCpuCoolerAsync(id);
                return Results.Ok($"Cpu cooler with id {id} deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        return webApplication;
    }
}
