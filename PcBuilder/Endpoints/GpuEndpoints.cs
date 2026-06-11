using Microsoft.AspNetCore.Mvc;
using PcBuilder.Models;
using PcBuilder.Services;
using PcBuilder.Services.Interfaces;
using System.Xml;

namespace PcBuilder.Endpoints;

public static class GpuEndpoints
{
    public static WebApplication MapGpuEndpoints(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("/gpus");

        group.MapGet(string.Empty, async ([FromServices]IGpuService gpuService, CancellationToken cancellationToken) =>
        {
        return Results.Ok(await gpuService.GetGpusAsync(cancellationToken));
        });

        group.MapGet("/{id}", async ([FromServices] IGpuService gpuService, int id, CancellationToken cancellationToken) =>
        {
                var gpu = await gpuService.GetGpuById(id, cancellationToken);
                return Results.Ok(gpu);
        });

        group.MapPost(string.Empty, async ([FromBody] GpuCreateRequest dto, [FromServices] IGpuService gpuService, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Gpu data is required");
                var gpu = await gpuService.AddGpuAsync(dto, cancellationToken);
                return Results.Ok(gpu);
        });

        group.MapPut("/{id}", async ([FromServices] IGpuService gpuService, [FromBody] GpuUpdateRequest dto, int id, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Gpu data is required");
                var gpu = await gpuService.UpdateGpuAsync(id, dto, cancellationToken);
                return Results.Ok(gpu);
        });

        group.MapDelete("/{id}", async ([FromServices] IGpuService gpuService, int id, CancellationToken cancellationToken) =>
        {
                await gpuService.DeleteGpuAsync(id, cancellationToken);
                return Results.Ok($"Gpu with id {id} deleted successfully");
        });


        
        return webApplication;
    }
}
