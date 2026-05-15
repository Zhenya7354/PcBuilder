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
            
            group.MapGet("/{id}", async ([FromServices] BuildService service, ClaimsPrincipal user, int id) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId is null)
                {
                    return Results.Unauthorized();
                }
                try
                {
                    var build = await service.GetBuildByIdAsync(userId, id);
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

            group.MapPut("/update/{id}", async ([FromServices] BuildService service, ClaimsPrincipal user, int id, [FromBody] BuildUpdateDto dto) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId is null)
                {
                    return Results.Unauthorized();
                }
                try
                {
                    var build = await service.UpdateBuildAsync(id, userId, dto);
                    return Results.Ok(build);
                }
                catch(KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            });

            group.MapDelete("/delete/{id}", async ([FromServices] BuildService service, ClaimsPrincipal user, int id) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId is null)
                {
                    return Results.Unauthorized();
                }
                try
                {
                    await service.DeleteBuildAsync(id, userId);
                    return Results.Ok();
                }
                catch(KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            });

            group.MapPost("/set-component/{id}", async ([FromServices] BuildService service, ClaimsPrincipal user, int id, [FromBody] BuildComponentDto dto) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId is null)
                {
                    return Results.Unauthorized();
                }
                try
                {
                    var build = await service.SetComponentAsync(id, userId, dto);
                    return Results.Ok(build);
                }
                catch(KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            });

            group.MapPost("/remove-component/{id}", async ([FromServices] BuildService service, ClaimsPrincipal user, int id, [FromBody] BuildComponentType componentType) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId is null)
                {
                    return Results.Unauthorized();
                }
                try
                {
                    var build = await service.RemoveComponentAsync(id, userId, componentType);
                    return Results.Ok(build);
                }
                catch(KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            });

            return app;
        }
    }
}
