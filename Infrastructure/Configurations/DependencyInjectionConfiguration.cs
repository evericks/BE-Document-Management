using Application.Services.Authentication.Implementations;
using Application.Services.Authentication.Interfaces;
using Application.Services.Evercloud.Services.Implementations;
using Application.Services.Evercloud.Services.Interfaces;
using Application.Services.Hangfire.Implementations;
using Application.Services.Hangfire.Interfaces;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using Data.UnitOfWorks.Implementations;
using Data.UnitOfWorks.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEvercloudService, EvercloudService>();
        services.AddScoped<IHangfireService, HangfireService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<IDocumentStatusService, DocumentStatusService>();
        services.AddScoped<IDocumentTypeService, DocumentTypeService>();
        services.AddScoped<IProcessService, ProcessService>();
        services.AddScoped<IOrganizationService, OrganizationService>();
    }
}