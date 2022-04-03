using GenerationRules;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddGenerationRules();
    }
}