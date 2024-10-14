using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations;

public static class CorsConfiguration
{
    public static void AddCorsWithOptions(this IServiceCollection services, string allowSpecificOrigins)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: allowSpecificOrigins,
                policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.WithExposedHeaders("*");
                });
        });
    }
}