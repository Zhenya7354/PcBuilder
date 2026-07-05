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
            [FromServices] IBuildRecommendationService recommendationService,
            [FromBody] AiBuildRequest request,
            CancellationToken cancellationToken
            ) =>
        {
            if (string.IsNullOrEmpty(request.Prompt))
                return Results.BadRequest(new { Message = "Prompt is required" });

            var requirements = await service.AnalyzeAsync(request.Prompt, cancellationToken);
            var recommendation = await recommendationService.RecommendBuildAsync(requirements, cancellationToken);

            return Results.Ok(recommendation);
        }).RequireAuthorization();

        return app;
    }
}
