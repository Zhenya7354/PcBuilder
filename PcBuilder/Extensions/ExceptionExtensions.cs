using PcBuilder.Exceptions;
using PcBuilder.Repositories;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Extensions;

public static class ExceptionExtensions
{
    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<UnauthorizedAccessExceptionHandler>();
        builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        return builder;
    }
}
