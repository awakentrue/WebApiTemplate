using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

                if (context.Database.IsSqlite())
                {
                    await context.Database.MigrateAsync();

                    var appliedMigrations = await context.Database.GetAppliedMigrationsAsync();
                    logger.LogInformation($"Applied Migrations count: {appliedMigrations.Count()}");
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "An error occurred during database migration");
                
                throw;
            }
        }

        await host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
    }
}