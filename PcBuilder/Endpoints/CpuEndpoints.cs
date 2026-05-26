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

        group.MapGet("", async ([FromServices] ICpuService cpuService) =>
        {
            var cpus = await cpuService.GetAllCpuAsync();
            if (cpus is null) return Results.NotFound("Cpus not found");
            return Results.Ok(cpus);
        });

        group.MapGet("/{id}", async ([FromServices] ICpuService cpuService, int id) =>
        {
            try
            {
                var cpu = await cpuService.GetCpuByIdAsync(id);
                return Results.Ok(cpu);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
        group.MapPost("", async ([FromServices] ICpuService cpuService, [FromBody] CpuCreate cpuDto) =>
        {
            if(cpuDto is null) return Results.BadRequest("Cpu data is required");
            try
            {
                var cpu = await cpuService.AddCpuAsync(cpuDto);
                return Results.Ok(cpu);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
        group.MapPut("/{id}", async ([FromServices] ICpuService cpuService, [FromBody] CpuUpdate cpuDto, int id) =>
        {
            var cpu = await cpuService.GetCpuByIdAsync(id);
            if (cpu is null) return Results.NotFound("Cpu not found");
            try
            {
                var updatedCpu = await cpuService.UpdateCpuAsync(id, cpuDto);
                return Results.Ok(updatedCpu);

            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] ICpuService cpuService, int id) =>
        {
            var cpu = await cpuService.GetCpuByIdAsync(id);
            if (cpu is null) return Results.NotFound("Cpu not found");
            try
            {
                await cpuService.DeleteCpuAsync(id);
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
