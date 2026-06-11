using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PcBuilder.Exceptions;

internal sealed class NotFoundExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<NotFoundExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
       if(exception is not KeyNotFoundException notFoundException)
        {
            return false;
        }

        logger.LogError(exception, "Error: {Message}, OcurredAt: {Time}", exception.Message, DateTime.Now);

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        { 
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Status = httpContext.Response.StatusCode,
                Title = "Resource not found",
                Detail = exception.Message,
            }
        });
    }
}
