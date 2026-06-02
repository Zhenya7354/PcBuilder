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

        group.MapGet(string.Empty, async ([FromServices]IGpuService gpuService) =>
        {
        return Results.Ok(await gpuService.GetGpusAsync());
        });

        group.MapGet("/{id}", async ([FromServices] IGpuService gpuService, int id) =>
        {
            try
            {
                var gpu = await gpuService.GetGpuById(id);
                return Results.Ok(gpu);
            }
            catch(KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost(string.Empty, async ([FromBody] GpuCreateRequest dto, [FromServices] IGpuService gpuService) =>
        {
            if (dto is null) return Results.BadRequest("Gpu data is required");
            try
            {
                var gpu = await gpuService.AddGpuAsync(dto);
                return Results.Ok(gpu);
            }
            catch(KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id}", async ([FromServices] IGpuService gpuService, [FromBody] GpuUpdateRequest dto, int id) =>
        {
            if (dto is null) return Results.BadRequest("Gpu data is required");
            try
            {
                var gpu = await gpuService.UpdateGpuAsync(id, dto);
                return Results.Ok(gpu);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] IGpuService gpuService, int id) =>
        {
            try
            {
                await gpuService.DeleteGpuAsync(id);
                return Results.Ok($"Gpu with id {id} deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });


        
        return webApplication;
    }
}
