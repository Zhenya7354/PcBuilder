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
                var builds = await service.GetUserBuildsAsync();
                return Results.Ok(builds);
            }
            catch (KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            catch (UnauthorizedAccessException) { return Results.Unauthorized(); }
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
                var build = await service.GetBuildByIdAsync(id);
                return Results.Ok(build);
            }
            catch (KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            catch (UnauthorizedAccessException) { return Results.Unauthorized(); }

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
                var build = await service.AddBuildAsync(dto);
                return Results.Ok(build);
            }
            catch (KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            catch (UnauthorizedAccessException) { return Results.Unauthorized(); }
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
                var issues = await service.RunCompatibilityChecksForUpdateAsync(id, dto);
                if (issues.Any(i => i.Severity == CompatibilityServerity.Error))
                {
                    return Results.BadRequest(new { Message = "Build has compatibility issues", Issues = issues });
                }
                if (issues.Any(i => i.Severity == CompatibilityServerity.Warning))
                {
                    return Results.Ok(new { Message = "Build has compatibility warnings", Issues = issues });
                }
                var build = await service.UpdateBuildAsync(id, dto);
                return Results.Ok(build);
            }
            catch (KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            catch (UnauthorizedAccessException) { return Results.Unauthorized(); }
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
                await service.DeleteBuildAsync(id);
                return Results.Ok();
            }
            catch (KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            catch (UnauthorizedAccessException) { return Results.Unauthorized(); }
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
                var issues = await service.RunCompatibilityChecksForComponentUpdateAsync(id, dto);
                if (issues.Any(i => i.Severity == CompatibilityServerity.Error))
                {
                    return Results.BadRequest(new { Message = "Build has compatibility issues", Issues = issues });
                }
                if (issues.Any(i => i.Severity == CompatibilityServerity.Warning))
                {
                    return Results.Ok(new { Message = "Build has compatibility warnings", Issues = issues });
                }
                var build = await service.SetComponentAsync(id, dto);
                return Results.Ok(build);
            }
            catch (KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            catch (UnauthorizedAccessException) { return Results.Unauthorized(); }
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
                var build = await service.RemoveComponentAsync(id, componentType);
                return Results.Ok(build);
            }
            catch (KeyNotFoundException ex) { return Results.NotFound(ex.Message); }
            catch (UnauthorizedAccessException) { return Results.Unauthorized(); }
        });

        return app;
    }
}