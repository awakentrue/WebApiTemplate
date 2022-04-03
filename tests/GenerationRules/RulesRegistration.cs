using GenerationRules.Rules;
using Microsoft.Extensions.DependencyInjection;

namespace GenerationRules;

public static class RulesRegistration
{
    public static IServiceCollection AddGenerationRules(this IServiceCollection services)
    {
        services.AddSingleton<BookGenerationRule>();

        return services;
    }
}