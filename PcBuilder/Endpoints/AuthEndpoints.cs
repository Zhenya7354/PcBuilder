using Microsoft.AspNetCore.Identity;
using PcBuilder.Data;
using PcBuilder.Models;
using PcBuilder.Services;

namespace PcBuilder.Endpoints
{
    public static class AuthEndpoints
    {
        public static WebApplication MapAuthEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/auth");


            group.MapPost("/register", async (RegisterDto dto, UserManager<User> userManager, PcDbContext db) =>
            {
                if (dto is null || userManager is null) return Results.BadRequest();

                var user = new User { UserName = dto.Email, Email = dto.Email };
                var result = await userManager.CreateAsync(user, dto.Password);

                if (!result.Succeeded) return Results.BadRequest(result.Errors);

                await userManager.AddToRoleAsync(user, "User");

                db.regular_user.Add(new RegularUser { userId = user.Id });
                await db.SaveChangesAsync();

                return Results.Ok(new { message = "Registration successful" });
            });


            group.MapPost("/login", async (
                LoginDto dto,
                SignInManager<User> singInManager,
                UserManager<User> userManager,
                JwtService jwtService) =>
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

                return Results.Ok(new AuthResponseDto(token, user.Email!, roles, expires));
            });

            // Endpoint for making user an admin, have to be moved in future
            app.MapPost("/admin/{userId}/make-admin", async (
                string userId,
                UserManager<User> userManager,
                PcDbContext db) =>
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user is null) return Results.NotFound();
                using var transaction = await db.Database.BeginTransactionAsync();
                try
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    db.admin.Add(new Admin { userId = user.Id });
                    await db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
                return Results.Ok(new { message = "User promoted to Admin" });
            }).RequireAuthorization();

            return app;
        }
    }
}
