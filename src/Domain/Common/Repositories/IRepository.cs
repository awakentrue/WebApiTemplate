namespace Domain.Common.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity : IEntity
{
    Task<TEntity> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> SaveAsync(TEntity[] entities, CancellationToken cancellationToken = default);
    void Delete(params TEntity[] entities);
    void Update(params TEntity[] entities);
}