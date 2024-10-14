using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Configurations;

public static class AuthorizationConfiguration
{
    public static void UseJwtAuthorization(this IApplicationBuilder app)
    {
        app.UseMiddleware<AuthorizationMiddleware>();
    }
}