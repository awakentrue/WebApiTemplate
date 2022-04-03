using Microsoft.AspNetCore.Mvc;

namespace WebApi.Configurations;

public static class ApiBehaviorConfiguration
{
    public static IServiceCollection AddApiBehaviorConfiguration(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        return services;
    }
}