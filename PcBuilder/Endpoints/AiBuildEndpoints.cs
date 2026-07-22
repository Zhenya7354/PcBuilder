using Microsoft.AspNetCore.Mvc;
using PcBuilder.Models;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Endpoints;

public static class AiBuildEndpoints
{
    public static WebApplication MapAiBuildEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/builds/ai");

        group.MapPost("/recommend", async (
            [FromServices] IAiBuildService service,
            [FromBody] AiBuildRequest request,
            CancellationToken cancellationToken
            ) =>
        {
            if (string.IsNullOrEmpty(request.Prompt))
                return Results.BadRequest(new { Message = "Prompt is required" });

            var recommendation = await service.RecommendBuildAsync(request.Prompt, cancellationToken);

            return Results.Ok(recommendation);
        }).RequireAuthorization();

        return app;
    }
}
