using Microsoft.AspNetCore.Mvc;
using PcBuilder.Models;
using PcBuilder.Services;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Endpoints;

public static class BrandEndpoints
{
    public static WebApplication MapBrandEndpoints(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("/brands");

        group.MapGet(string.Empty, async ([FromServices] IBrandService brandService) =>
        {
            var brands = await brandService.GetAllBrandsAsync();
            return Results.Ok(brands);
        });

        group.MapGet("/{id}", async ([FromServices] IBrandService brandService, int id) =>
        {
            try
            {
                var brand = await brandService.GetBrandByIdAsync(id);
                return Results.Ok(brand);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost(string.Empty, async ([FromServices] IBrandService brandService, [FromBody] BrandCreate dto) =>
        {
            if (dto is null) return Results.BadRequest("Brand data is required");
            try
            {
                var brand = await brandService.AddBrandAsync(dto);
                return Results.Ok(brand);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id}", async ([FromServices] IBrandService brandService, [FromBody] BrandUpdate dto, int id) =>
        {
            if (dto is null) return Results.BadRequest("Brand data is required");
            try
            {
                var brand = await brandService.UpdateBrandAsync(id, dto);
                return Results.Ok(brand);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] IBrandService brandService, int id) =>
        {
            try
            {
                await brandService.DeleteBrandAsync(id);
                return Results.Ok($"Brand with id {id} deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        return webApplication;
    }
}
