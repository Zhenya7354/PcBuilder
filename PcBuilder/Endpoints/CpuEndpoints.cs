using Microsoft.AspNetCore.Mvc;
using PcBuilder.Models;
using PcBuilder.Services;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Endpoints;

public static class CpuEndpoints
{
    public static WebApplication MapCpuEndpoints(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("/cpus");

        group.MapGet(string.Empty, async ([FromServices] ICpuService cpuService, CancellationToken cancellationToken) =>
        {
            return Results.Ok(await cpuService.GetAllCpuAsync(cancellationToken));
        });

        group.MapGet("/{id}", async ([FromServices] ICpuService cpuService, int id, CancellationToken cancellationToken) =>
        {
            try
            {
                var cpu = await cpuService.GetCpuByIdAsync(id, cancellationToken);
                return Results.Ok(cpu);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
        group.MapPost(string.Empty, async ([FromServices] ICpuService cpuService, [FromBody] CpuCreateRequest cpuDto, CancellationToken cancellationToken) =>
        {
            if(cpuDto is null) return Results.BadRequest("Cpu data is required");
            try
            {
                var cpu = await cpuService.AddCpuAsync(cpuDto, cancellationToken);
                return Results.Ok(cpu);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
        group.MapPut("/{id}", async ([FromServices] ICpuService cpuService, [FromBody] CpuUpdateRequest cpuDto, int id, CancellationToken cancellationToken) =>
        {
            var cpu = await cpuService.GetCpuByIdAsync(id, cancellationToken);
            if (cpu is null) return Results.NotFound("Cpu not found");
            try
            {
                var updatedCpu = await cpuService.UpdateCpuAsync(id, cpuDto, cancellationToken);
                return Results.Ok(updatedCpu);

            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] ICpuService cpuService, int id, CancellationToken cancellationToken) =>
        {
            var cpu = await cpuService.GetCpuByIdAsync(id, cancellationToken);
            if (cpu is null) return Results.NotFound("Cpu not found");
            try
            {
                await cpuService.DeleteCpuAsync(id, cancellationToken);
                return Results.Ok($"Cpu with id {id} deleted successfully");

            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
            return webApplication;
    }
}
