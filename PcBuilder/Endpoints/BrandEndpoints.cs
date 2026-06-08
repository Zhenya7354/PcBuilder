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

        group.MapGet(string.Empty, async ([FromServices] IBrandService brandService, CancellationToken cancellationToken) =>
        {
            var brands = await brandService.GetAllBrandsAsync(cancellationToken);
            return Results.Ok(brands);
        });

        group.MapGet("/{id}", async ([FromServices] IBrandService brandService, int id, CancellationToken cancellationToken) =>
        {
            try
            {
                var brand = await brandService.GetBrandByIdAsync(id, cancellationToken);
                return Results.Ok(brand);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost(string.Empty, async ([FromServices] IBrandService brandService, [FromBody] BrandCreateRequest dto, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Brand data is required");
            try
            {
                var brand = await brandService.AddBrandAsync(dto, cancellationToken);
                return Results.Ok(brand);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id}", async ([FromServices] IBrandService brandService, [FromBody] BrandUpdateRequest dto, int id, CancellationToken cancellationToken) =>
        {
            if (dto is null) return Results.BadRequest("Brand data is required");
            try
            {
                var brand = await brandService.UpdateBrandAsync(id, dto, cancellationToken);
                return Results.Ok(brand);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapDelete("/{id}", async ([FromServices] IBrandService brandService, int id, CancellationToken cancellationToken) =>
        {
            try
            {
                await brandService.DeleteBrandAsync(id, cancellationToken);
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
