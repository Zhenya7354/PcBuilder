using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PcBuilder.Exceptions;

internal sealed class UnauthorizedAccessExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<UnauthorizedAccessExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not UnauthorizedAccessException unauthorizedAccessException)
        {
            return false;
        }

        logger.LogError(exception, "Error: {Message}, OcurredAt: {Time}", exception.Message, DateTime.Now);

        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Status = httpContext.Response.StatusCode,
                Title = "Unauthorized",
                Detail = exception.Message,
            }
        });
    }
}
