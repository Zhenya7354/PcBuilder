using Microsoft.AspNetCore.Mvc;
using PcBuilder.Dtos;
using PcBuilder.Services;
using System.Xml;

namespace PcBuilder.Endpoints;

public static class GpuEndpoints
{
    public static WebApplication MapGpuEndpoints(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("/gpu");

        group.MapGet("/all", async ([FromServices]GpuService gpuService) =>
        {
            var gpus = await gpuService.GetGpusAsync();
            if(gpus is null) return Results.NotFound("Gpus not found");
        return Results.Ok(gpus);
        });

        group.MapGet("/{id}", async ([FromServices] GpuService gpuService, int id) =>
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

        group.MapPost("/add", async ([FromBody] GpuCreateDto dto, [FromServices] GpuService gpuService) =>
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

        group.MapPut("/update/{id}", async ([FromServices] GpuService gpuService, [FromBody] GpuUpdateDto dto, int id) =>
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

        group.MapDelete("/delete/{id}", async ([FromServices] GpuService gpuService, int id) =>
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
