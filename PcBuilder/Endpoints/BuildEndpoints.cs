using Microsoft.AspNetCore.Mvc;
using PcBuilder.Dtos;
using PcBuilder.Enums;
using PcBuilder.Services;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace PcBuilder.Endpoints
{
    public static class BuildEndpoints
    {
        public static WebApplication MapBuildEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/build");

            group.MapGet("/all", async ([FromServices] BuildService service, ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId is null)
                {
                    return Results.Unauthorized();
                }
                try
                {
                    var builds = await service.GetUserBuildsAsync(userId);
                    return Results.Ok(builds);
                }
                catch(KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            });
            
            group.MapGet("/{id}", async ([FromServices] BuildService service, ClaimsPrincipal user, int buildId) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId is null)
                {
                    return Results.Unauthorized();
                }
                try
                {
                    var build = await service.GetBuildByIdAsync(userId, buildId);
                    return Results.Ok(build);
                }
                catch(KeyNotFoundException ex) { return Results.NotFound(ex.Message); }

            });

            group.MapPost("/add", async ([FromServices] BuildService service, ClaimsPrincipal user, [FromBody] BuildCreateDto dto) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId is null)
                {
                    return Results.Unauthorized();
                }
                try
                {
                    var build = await service.AddBuildAsync(userId, dto);
                    return Results.Ok(build);
                }
                catch(KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            });

            group.MapPut("/update/{id}", async ([FromServices] BuildService service, ClaimsPrincipal user, int buildId, [FromBody] BuildUpdateDto dto) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId is null)
                {
                    return Results.Unauthorized();
                }
                try
                {
                    var build = await service.UpdateBuildAsync(buildId, userId, dto);
                    return Results.Ok(build);
                }
                catch(KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            });

            group.MapDelete("/delete/{id}", async ([FromServices] BuildService service, ClaimsPrincipal user, int buildId) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId is null)
                {
                    return Results.Unauthorized();
                }
                try
                {
                    await service.DeleteBuildAsync(buildId, userId);
                    return Results.Ok();
                }
                catch(KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            });

            group.MapPost("/set-component/{id}", async ([FromServices] BuildService service, ClaimsPrincipal user, int buildId, [FromBody] BuildComponentDto dto) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId is null)
                {
                    return Results.Unauthorized();
                }
                try
                {
                    var build = await service.SetComponentAsync(buildId, userId, dto);
                    return Results.Ok(build);
                }
                catch(KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            });

            group.MapPost("/remove-component/{id}", async ([FromServices] BuildService service, ClaimsPrincipal user, int buildId, [FromBody] BuildComponentType componentType) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId is null)
                {
                    return Results.Unauthorized();
                }
                try
                {
                    var build = await service.RemoveComponentAsync(buildId, userId, componentType);
                    return Results.Ok(build);
                }
                catch(KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            });

            return app;
        }
    }
}
