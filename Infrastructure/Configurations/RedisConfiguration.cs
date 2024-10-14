using Application.Services.Cache.Implementations;
using Application.Services.Cache.Interfaces;
using Application.Settings;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure.Configurations;

public static class RedisConfiguration
{
    public static void AddRedis(this IServiceCollection services)
    {
        var redisSettings = services.BuildServiceProvider().GetService<RedisSettings>();
        if (redisSettings == null) return;
        var redistConfiguration = redisSettings;
        if (!redistConfiguration.Enabled) return;
        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(redistConfiguration.ConnectionString));
        services.AddStackExchangeRedisCache(option => option.Configuration = redistConfiguration.ConnectionString);
        services.AddSingleton<IResponseCacheService, ResponseCacheService>();
    }
}