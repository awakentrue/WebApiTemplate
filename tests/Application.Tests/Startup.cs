using GenerationRules;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationLayer();
        services.AddGenerationRules();
    }
}