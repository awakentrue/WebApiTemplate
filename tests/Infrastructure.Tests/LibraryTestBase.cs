using System;
using System.Threading.Tasks;
using Domain.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tests;

public class LibraryTestBase : IDisposable
{
    protected readonly ApplicationDbContext Context;
    
    protected LibraryTestBase()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();
        
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .UseInternalServiceProvider(serviceProvider);

        Context = new ApplicationDbContext(builder.Options);
        Context.Database.EnsureCreated();
    }

    protected async Task SaveEntitiesAsync<TEntity>(params TEntity[] entities) where TEntity : class, IEntity
    {
        await Context.Set<TEntity>().AddRangeAsync(entities);
        await Context.SaveChangesAsync();
    }

    protected async Task<TEntity> GetEntity<TEntity>(int id) where TEntity : class, IEntity
    {
        return await Context.Set<TEntity>().SingleAsync(x => x.Id == id);
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}