using Application.Services.Authentication.Interfaces;
using Application.Settings;
using Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Middlewares;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public AuthorizationMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IAuthenticationService authenticationService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            await AttachUserToContext(context, authenticationService, token);
        }
        await _next(context);
    }
    
    private async Task AttachUserToContext(HttpContext context, IAuthenticationService authenticationService, string token)
    {
        var securityToken = JwtHelper.ValidateJwtToken(token, _appSettings.Secret);
        var userId = Guid.Parse(securityToken.Claims.First(x => x.Type == "id").Value);
        // Get user information
        var user = await authenticationService.GetUser(userId);
        // Push the user into context
        context.Items["USER"] = user;
    }
}