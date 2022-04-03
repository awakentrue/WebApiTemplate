using Domain.Common;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryDbContext _dbContext;

    public UnitOfWork(LibraryDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public void Dispose()
    {
        _dbContext.Dispose();
    }
}