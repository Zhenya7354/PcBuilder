using PcBuilder.Exceptions;
using PcBuilder.Exceptions.ExceptionHandlers;
using PcBuilder.Repositories;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Extensions;

public static class ExceptionExtensions
{
    public static WebApplicationBuilder AddExceptionsServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<UnauthorizedAccessExceptionHandler>();
        builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        return builder;
    }
}
