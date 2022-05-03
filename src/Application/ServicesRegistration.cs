using System.Reflection;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServicesRegistration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        
        services.AddMediatR(executingAssembly);
        services.AddFluentValidationConfiguration(executingAssembly);
        services.AddMapster(executingAssembly);

        return services;
    }

    private static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddFluentValidation(x =>
        {
            x.DisableDataAnnotationsValidation = true;
            x.RegisterValidatorsFromAssemblies(assemblies);
        });

        return services;
    }
    
    private static IServiceCollection AddMapster(this IServiceCollection services, params Assembly[] assemblies)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        var registers = config.Scan(assemblies);

        config.Apply(registers);

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        
        return services;
    }
}