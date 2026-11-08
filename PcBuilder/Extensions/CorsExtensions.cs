namespace PcBuilder.Extentions;

public static class CorsExtensions
{
    public const string FrontendPolicyName = "Frontend";

    private static readonly string[] FrontendOrigins =
    [
        "http://localhost:5173",
        "http://localhost:5174"
    ];

    public static WebApplicationBuilder AddFrontendCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(FrontendPolicyName, policy =>
            {
                policy.WithOrigins(FrontendOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return builder;
    }
}
