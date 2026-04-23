using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Services;

namespace PcBuilder.Extentions
{
    public static class ServiceExtentions
    {
        public static WebApplicationBuilder AddAppServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<JwtService>();
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<PcDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            return builder;
        }
    }
}
