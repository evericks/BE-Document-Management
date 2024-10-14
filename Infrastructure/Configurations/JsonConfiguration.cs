using Application.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations;

public static class JsonConfiguration
{
    public static void AddJsonSettings(this IServiceCollection services, WebApplicationBuilder builder)
    {
        // App settings
        services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

        // Redis settings
        var redisSettings = new RedisSettings
        {
            Enabled = false,
            ConnectionString = string.Empty
        };
        builder.Configuration.GetSection("RedisSettings").Bind(redisSettings);
        services.AddSingleton(redisSettings);
    }
}