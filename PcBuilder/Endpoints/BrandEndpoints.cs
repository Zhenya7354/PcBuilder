using Microsoft.AspNetCore.Mvc;
using PcBuilder.Dtos;
using PcBuilder.Services;

namespace PcBuilder.Endpoints
{
    public static class BrandEndpoints
    {
        public static WebApplication MapBrandEndpoints(this WebApplication webApplication)
        {
            var group = webApplication.MapGroup("/brand");

            group.MapGet("/all", async ([FromServices] BrandService brandService) =>
            {
                var brands = await brandService.GetAllBrandsAsync();
                return Results.Ok(brands);
            });

            group.MapGet("/{id}", async ([FromServices] BrandService brandService, int id) =>
            {
                try
                {
                    var brand = await brandService.GetBrandByIdAsync(id);
                    return Results.Ok(brand);
                }
                catch (ArgumentException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            group.MapPost("/add", async ([FromServices] BrandService brandService, [FromBody] BrandCreateDto dto) =>
            {
                if (dto is null) return Results.BadRequest("Brand data is required");
                var brand = await brandService.AddBrandAsync(dto);
                return Results.Ok(brand);
            });

            group.MapPut("/update/{id}", async ([FromServices] BrandService brandService, [FromBody] BrandUpdateDto dto, int id) =>
            {
                if (dto is null) return Results.BadRequest("Brand data is required");
                try
                {
                    var brand = await brandService.UpdateBrandAsync(id, dto);
                    return Results.Ok(brand);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapDelete("/delete/{id}", async ([FromServices] BrandService brandService, int id) =>
            {
                try
                {
                    await brandService.DeleteBrandAsync(id);
                    return Results.Ok($"Brand with id {id} deleted successfully");
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            return webApplication;
        }
    }
}
