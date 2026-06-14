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

        group.MapGet(string.Empty, async ([FromServices] IBuildService service, ClaimsPrincipal user, CancellationToken cancellationToken) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            var builds = await service.GetUserBuildsAsync(cancellationToken);
            return Results.Ok(builds);
        });

        group.MapGet("/{id}", async ([FromServices] IBuildService service, ClaimsPrincipal user, int id, CancellationToken cancellationToken) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            var build = await service.GetBuildByIdAsync(id, cancellationToken);
            return Results.Ok(build);
        });

        group.MapPost(string.Empty, async ([FromServices] IBuildService service, ClaimsPrincipal user, [FromBody] BuildRequest dto, CancellationToken cancellationToken) =>
        {

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var issues = await service.RunCompatibilityChecksAsync(dto, cancellationToken);
            if (issues.Any(i => i.Severity == CompatibilityServerity.Error))
            {
                return Results.BadRequest(new { Message = "Build has compatibility issues", Issues = issues });
            }
            if (issues.Any(i => i.Severity == CompatibilityServerity.Warning))
            {
                return Results.Ok(new { Message = "Build has compatibility warnings", Issues = issues });
            }
            var build = await service.AddBuildAsync(dto, cancellationToken);
            return Results.Ok(build);
        });

        group.MapPut("/{id}", async ([FromServices] IBuildService service, ClaimsPrincipal user, int id, [FromBody] BuildRequest dto, CancellationToken cancellationToken) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            var issues = await service.RunCompatibilityChecksForBuildUpdateAsync(id, dto, cancellationToken);
            if (issues.Any(i => i.Severity == CompatibilityServerity.Error))
            {
                return Results.BadRequest(new { Message = "Build has compatibility issues", Issues = issues });
            }
            if (issues.Any(i => i.Severity == CompatibilityServerity.Warning))
            {
                return Results.Ok(new { Message = "Build has compatibility warnings", Issues = issues });
            }
            var build = await service.UpdateBuildAsync(id, dto, cancellationToken);
            return Results.Ok(build);
        });

        group.MapDelete("/{id}", async ([FromServices] IBuildService service, ClaimsPrincipal user, int id, CancellationToken cancellationToken) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            await service.DeleteBuildAsync(id, cancellationToken);
            return Results.Ok();
        });

        group.MapPost("/{id}/components", async ([FromServices] IBuildService service, ClaimsPrincipal user, int id, [FromBody] BuildComponentRequest dto, CancellationToken cancellationToken) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            var issues = await service.RunCompatibilityChecksForComponentUpdateAsync(id, dto, cancellationToken);
            if (issues.Any(i => i.Severity == CompatibilityServerity.Error))
            {
                return Results.BadRequest(new { Message = "Build has compatibility issues", Issues = issues });
            }
            if (issues.Any(i => i.Severity == CompatibilityServerity.Warning))
            {
                return Results.Ok(new { Message = "Build has compatibility warnings", Issues = issues });
            }
            var build = await service.SetComponentAsync(id, dto, cancellationToken);
            return Results.Ok(build);
        });

        group.MapDelete("/{id}/components", async ([FromServices] IBuildService service, ClaimsPrincipal user, int id, [FromBody] BuildComponentType componentType, CancellationToken cancellationToken) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            var build = await service.RemoveComponentAsync(id, componentType, cancellationToken);
            return Results.Ok(build);
        });

        return app;
    }
}