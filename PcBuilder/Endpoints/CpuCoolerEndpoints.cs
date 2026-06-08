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

        group.MapGet(string.Empty, async ([FromServices] ICpuCoolerService service, CancellationToken cancellationToken) =>
        {
            return Results.Ok(await service.GetAllCpuCoolersAsync(cancellationToken));
        });

        group.MapGet("/{id}", async ([FromServices] ICpuCoolerService service, int id, CancellationToken cancellationToken) =>
        {
            try
            {
                var cpuCooler = await service.GetCpuCoolerByIdAsync(id, cancellationToken);
                return Results.Ok(cpuCooler);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost(string.Empty, async ([FromServices] ICpuCoolerService service, [FromBody] CpuCoolerCreateRequest dto, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Cpu cooler data is required");
            try
            {
                var cpuCooler = await service.AddCpuCoolerAsync(dto, cancellationToken);
                return Results.Ok(cpuCooler);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id}", async ([FromServices] ICpuCoolerService service, [FromBody] CpuCoolerUpdateRequest dto, int id, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Cpu cooler data is required");
            try
            {
                var cpuCooler = await service.UpdateCpuCoolerAsync(id, dto, cancellationToken);
                return Results.Ok(cpuCooler);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] ICpuCoolerService service, int id, CancellationToken cancellationToken) =>
        {
            try
            {
                await service.DeleteCpuCoolerAsync(id, cancellationToken);
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
