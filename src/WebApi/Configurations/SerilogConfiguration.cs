using Serilog;
using Serilog.Events;

namespace WebApi.Configurations;

public static class SerilogConfiguration
{
    public static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(builder =>
        {
            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    path: "../logs/api-.log",
                    rollingInterval: RollingInterval.Month,
                    restrictedToMinimumLevel: LogEventLevel.Information,
                    shared: true)
                .CreateLogger();
            
            builder.AddSerilog(logger);
        });

        return services;
    }
}