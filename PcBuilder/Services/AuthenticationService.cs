using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PcBuilder.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PcBuilder.Services
{
    public class AuthenticationService
    {
        public record Request(string Email, string Password);
        public static User user = new();

        public static void MapRegisterUserEndpoint(IEndpointRouteBuilder app)
        {
            //app.MapPost("register", async (Request request, UserManager<User> userManager, IConfiguration configuration) =>
            //    {
            //        var user = await userManager.FindByEmailAsync(request.Email);
            //        if (user is null || !(await userManager.CheckPasswordAsync(user, request.Password)))
            //        {
            //            return TypedResults.Unauthorized();
            //        }
            //        var roles = await userManager.GetRolesAsync(user);

            //        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!));
            //        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                    
                        
                    
            //    });
        }
    }
}
