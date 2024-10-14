using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations;

public static class FluentValidationConfiguration
{
  public static void AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        // services.AddValidatorsFromAssembly(typeof(StudentCreateValidator).Assembly);
    }
}