using Application.Services.Hangfire.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations;

public static class HangfireConfiguration
{
    public static void AddHangfire(this IServiceCollection services, string? connectionString)
    {
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(connectionString));
    }
    
    public static void UseHangfire(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var services = serviceScope.ServiceProvider;
        var hangfireService = services.GetService<IHangfireService>();
        // Tasks
        hangfireService?.DefaultTask();
    }
}