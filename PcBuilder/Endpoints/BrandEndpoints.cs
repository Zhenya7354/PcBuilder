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
                var brand = await brandService.GetBrandByIdAsync(id, cancellationToken);
                return Results.Ok(brand);
        });

        group.MapPost(string.Empty, async ([FromServices] IBrandService brandService, [FromBody] BrandCreateRequest dto, CancellationToken cancellationToken) =>
        {
                var brand = await brandService.AddBrandAsync(dto, cancellationToken);
                return Results.Ok(brand);
        });

        group.MapPut("/{id}", async ([FromServices] IBrandService brandService, [FromBody] BrandUpdateRequest dto, int id, CancellationToken cancellationToken) =>
        {
                var brand = await brandService.UpdateBrandAsync(id, dto, cancellationToken);
                return Results.Ok(brand);
        });

        group.MapDelete("/{id}", async ([FromServices] IBrandService brandService, int id, CancellationToken cancellationToken) =>
        {
                await brandService.DeleteBrandAsync(id, cancellationToken);
                return Results.Ok($"Brand with id {id} deleted successfully");
        });

        return webApplication;
    }
}
