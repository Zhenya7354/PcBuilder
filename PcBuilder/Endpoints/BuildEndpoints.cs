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

        group.MapGet(string.Empty, async ([FromServices] IBuildService service, CancellationToken cancellationToken) =>
        {
            var builds = await service.GetUserBuildsAsync(cancellationToken);
            return Results.Ok(builds);
        }).RequireAuthorization();

        group.MapGet("/{id}", async ([FromServices] IBuildService service, int id, CancellationToken cancellationToken) =>
        {
            var build = await service.GetBuildByIdAsync(id, cancellationToken);
            return Results.Ok(build);
        }).RequireAuthorization();

        group.MapPost(string.Empty, async ([FromServices] IBuildService service, [FromBody] BuildRequest dto, CancellationToken cancellationToken) =>
        {
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
        }).RequireAuthorization();

        group.MapPut("/{id}", async ([FromServices] IBuildService service, int id, [FromBody] BuildRequest dto, CancellationToken cancellationToken) =>
        {
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
        }).RequireAuthorization();

        group.MapDelete("/{id}", async ([FromServices] IBuildService service, int id, CancellationToken cancellationToken) =>
        { 
            await service.DeleteBuildAsync(id, cancellationToken);
            return Results.Ok();
        }).RequireAuthorization();

        group.MapPost("/{id}/components", async ([FromServices] IBuildService service, int id, [FromBody] BuildComponentRequest dto, CancellationToken cancellationToken) =>
        {
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
        }).RequireAuthorization();

        group.MapDelete("/{id}/components", async ([FromServices] IBuildService service, int id, [FromBody] BuildComponentType componentType, CancellationToken cancellationToken) =>
        {
            var build = await service.RemoveComponentAsync(id, componentType, cancellationToken);
            return Results.Ok(build);
        }).RequireAuthorization();

        return app;
    }
}