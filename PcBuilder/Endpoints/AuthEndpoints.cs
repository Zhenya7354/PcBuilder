using Microsoft.AspNetCore.Identity;
using PcBuilder.Data;
using PcBuilder.Models;
using PcBuilder.Entities;
using PcBuilder.Services;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PcBuilder.Endpoints;

public static class AuthEndpoints
{
    public static WebApplication MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/auth");


        group.MapPost("/register", async (
            [FromBody] RegisterRequest dto,
            [FromServices] IAuthService authService,
            CancellationToken cancellationToken) =>
        {
            await authService.RegisterAsync(dto, cancellationToken);
            return Results.Ok(new { message = "Registration successful" });
        });


        group.MapPost("/login", async (
            [FromBody] LoginRequest dto,
            [FromServices] IAuthService authService,
            CancellationToken cancellationToken) =>
        {
            var authResponse = await authService.LoginAsync(dto, cancellationToken);
            return Results.Ok(authResponse);
        });

        // Endpoint for making user an admin, have to be moved elsewhere in future
        app.MapPost("/admin/{userId}/make-admin", async (
            string userId,
            IAdminService adminService,
            CancellationToken cancellationToken) =>
        {
            await adminService.PromoteToAdminAsync(userId, cancellationToken);
            return Results.Ok(new { message = "User promoted to Admin" });
        }).RequireAuthorization();

        return app;
    }
}