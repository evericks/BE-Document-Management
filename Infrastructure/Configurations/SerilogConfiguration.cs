using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Infrastructure.Configurations;

public static class SerilogConfiguration
{
    public static void AddSerilog(this ConfigureHostBuilder host)
    {
        host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration)
        );
    }
}