using Domain.Common;
using Domain.Common.Exceptions;
using Domain.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly ApplicationDbContext _dbContext;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        DbSet = dbContext.Set<TEntity>();
    }
    
    protected DbSet<TEntity> DbSet { get; }

    public async Task<TEntity> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync(new object[] { id }, cancellationToken) ?? throw new EntityNotFoundException(typeof(TEntity).Name, id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public async Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        
        return entity;
    }

    public async Task<IEnumerable<TEntity>> SaveAsync(TEntity[] entities, CancellationToken cancellationToken = default)
    {
        await DbSet.AddRangeAsync(entities, cancellationToken);
        
        return entities;
    }

    public void Delete(params TEntity[] entities)
    {
        DbSet.RemoveRange(entities);
    }

    public void Update(params TEntity[] entities)
    {
        DbSet.UpdateRange(entities);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}