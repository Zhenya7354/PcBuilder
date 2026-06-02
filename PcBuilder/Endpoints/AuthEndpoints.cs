using Microsoft.AspNetCore.Identity;
using PcBuilder.Data;
using PcBuilder.Models;
using PcBuilder.Entities;
using PcBuilder.Services;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Endpoints;

public static class AuthEndpoints
{
    public static WebApplication MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/auth");


        group.MapPost("/register", async (RegisterRequest dto, UserManager<UserEntity> userManager, PcDbContext db) =>
        {
            if (dto is null || userManager is null) return Results.BadRequest();

            var user = new UserEntity { UserName = dto.Email, Email = dto.Email };
            var result = await userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded) return Results.BadRequest(result.Errors);

            await userManager.AddToRoleAsync(user, "User");

            db.RegularUser.Add(new RegularUserEntity { UserId = user.Id });
            await db.SaveChangesAsync();

            return Results.Ok(new { message = "Registration successful" });
        });


        group.MapPost("/login", async (
            LoginRequest dto,
            SignInManager<UserEntity> singInManager,
            UserManager<UserEntity> userManager,
            IJwtService jwtService) =>
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user is null) return Results.Unauthorized();

            var result = await singInManager.CheckPasswordSignInAsync(user, dto.Password, true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    return Results.Json(
                        new { error = "Account locked. Try again later." },
                        statusCode: 429);
                return Results.Unauthorized();
            }
            var roles = await userManager.GetRolesAsync(user);
            var token = jwtService.GenerateToken(user, roles);
            var expires = DateTime.UtcNow.AddMinutes(60);

            return Results.Ok(new AuthResponse(token, user.Email!, roles, expires));
        });

        // Endpoint for making user an admin, have to be moved elsewhere in future
        app.MapPost("/admin/{userId}/make-admin", async (
            string userId,
            IAdminService adminService) =>
        {
            await adminService.PromoteToAdminAsync(userId);
            return Results.Ok(new { message = "User promoted to Admin" });
        }).RequireAuthorization();

        return app;
    }
}