using Microsoft.AspNetCore.Mvc;
using PcBuilder.Models;
using PcBuilder.Enums;
using PcBuilder.Services;
using PcBuilder.Services.Interfaces;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace PcBuilder.Endpoints;

public static class BuildEndpoints
{
    public static WebApplication MapBuildEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/builds");

        group.MapGet(string.Empty, async ([FromServices] IBuildService service, ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            try
            {
                var builds = await service.GetUserBuildsAsync(userId);
                return Results.Ok(builds);
            }
            catch (KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
        });

        group.MapGet("/{id}", async ([FromServices] IBuildService service, ClaimsPrincipal user, int id) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            try
            {
                var build = await service.GetBuildByIdAsync(userId, id);
                return Results.Ok(build);
            }
            catch (KeyNotFoundException ex) { return Results.NotFound(ex.Message); }

        });

        group.MapPost(string.Empty, async ([FromServices] IBuildService service, ClaimsPrincipal user, [FromBody] BuildRequest dto) =>
        {

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            try
            {
                var issues = await service.RunCompatibilityChecksAsync(dto);
                if (issues.Any(i => i.Severity == CompatibilityServerity.Error))
                {
                    return Results.BadRequest(new { Message = "Build has compatibility issues", Issues = issues });
                }
                if (issues.Any(i => i.Severity == CompatibilityServerity.Warning))
                {
                    return Results.Ok(new { Message = "Build has compatibility warnings", Issues = issues });
                }
                var build = await service.AddBuildAsync(userId, dto);
                return Results.Ok(build);
            }
            catch (KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
        });

        group.MapPut("/{id}", async ([FromServices] IBuildService service, ClaimsPrincipal user, int id, [FromBody] BuildRequest dto) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            try
            {
                var issues = await service.RunCompatibilityChecksForUpdateAsync(id, userId, dto);
                if (issues.Any(i => i.Severity == CompatibilityServerity.Error))
                {
                    return Results.BadRequest(new { Message = "Build has compatibility issues", Issues = issues });
                }
                if (issues.Any(i => i.Severity == CompatibilityServerity.Warning))
                {
                    return Results.Ok(new { Message = "Build has compatibility warnings", Issues = issues });
                }
                var build = await service.UpdateBuildAsync(id, userId, dto);
                return Results.Ok(build);
            }
            catch (KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
        });

        group.MapDelete("/{id}", async ([FromServices] IBuildService service, ClaimsPrincipal user, int id) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            try
            {
                await service.DeleteBuildAsync(id, userId);
                return Results.Ok();
            }
            catch (KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
        });

        group.MapPost("/{id}/components", async ([FromServices] IBuildService service, ClaimsPrincipal user, int id, [FromBody] BuildComponentRequest dto) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            try
            {
                var issues = await service.RunCompatibilityChecksForComponentUpdateAsync(id, userId, dto);
                if (issues.Any(i => i.Severity == CompatibilityServerity.Error))
                {
                    return Results.BadRequest(new { Message = "Build has compatibility issues", Issues = issues });
                }
                if (issues.Any(i => i.Severity == CompatibilityServerity.Warning))
                {
                    return Results.Ok(new { Message = "Build has compatibility warnings", Issues = issues });
                }
                var build = await service.SetComponentAsync(id, userId, dto);
                return Results.Ok(build);
            }
            catch (KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
        });

        group.MapDelete("/{id}/components", async ([FromServices] IBuildService service, ClaimsPrincipal user, int id, [FromBody] BuildComponentType componentType) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            try
            {
                var build = await service.RemoveComponentAsync(id, userId, componentType);
                return Results.Ok(build);
            }
            catch (KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
        });

        return app;
    }
}