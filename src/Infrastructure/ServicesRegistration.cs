using Domain.Common;
using Domain.Common.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServicesRegistration
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // var connectionString = configuration.GetConnectionString("DefaultConnection");
        // services.AddDbContext<LibraryDbContext>(options => options.UseSqlite(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        services.AddDbContext<LibraryDbContext>(options => options.UseInMemoryDatabase("db").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IBookRepository, BookRepository>();
        
        return services;
    }
}