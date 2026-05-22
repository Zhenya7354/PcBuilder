using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using PcBuilder.Data;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PcBuilder.Entities;

namespace PcBuilder.Extentions;

public static class IdentityExtentions
{
    private const string AdminPolicyName = "AdminOnly";
    private const string AuthenticatedUserPolicyName = "Authenticated";
    public static WebApplicationBuilder AddIdentityAndJwt(this WebApplicationBuilder builder)
    {
        var identitySection = builder.Configuration.GetSection("Identity");
        builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
        {
            options.Password.RequireDigit = bool.Parse(identitySection.GetSection("Password")["RequireDigit"]!);
            options.Password.RequiredLength = int.Parse(identitySection.GetSection("Password")["RequiredLength"]!);
            options.Password.RequireUppercase = bool.Parse(identitySection.GetSection("Password")["RequireUppercase"]!);
            options.Password.RequireNonAlphanumeric = bool.Parse(identitySection.GetSection("Password")["RequireNonAlphanumeric"]!);
            options.Lockout.MaxFailedAccessAttempts = int.Parse(identitySection.GetSection("Lockout")["MaxFailedAccessAttempts"]!);
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(int.Parse(identitySection.GetSection("Lockout")["DefaultLockoutTimeSpan"]!));
            options.User.RequireUniqueEmail = bool.Parse(identitySection.GetSection("User")["RequireUniqueEmail"]!);
        }
        ).AddEntityFrameworkStores<PcDbContext>()
        .AddDefaultTokenProviders();

        var jwtSection = builder.Configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSection["Key"]!);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSection["Issuer"],
                    ValidAudience = jwtSection["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.FromSeconds(30)
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async ctx =>
                    {
                        ctx.HandleResponse();
                        ctx.Response.StatusCode = 401;
                        ctx.Response.ContentType = "application/json";
                        await ctx.Response.WriteAsync("""{"\"error\": \"Unauthorized\"}""");
                    },
                    OnForbidden = async ctx =>
                    {
                        ctx.Response.StatusCode = 403;
                        ctx.Response.ContentType = "application/json";
                        await ctx.Response.WriteAsync("""{"\"error\": \"Forbidden\"}""");
                    }
                };
            });
        builder.Services.AddAuthorizationBuilder()
            .AddPolicy(AdminPolicyName, policy => policy.RequireRole("Admin"))
            .AddPolicy(AuthenticatedUserPolicyName, policy => policy.RequireAuthenticatedUser());

        return builder;
    }
}
