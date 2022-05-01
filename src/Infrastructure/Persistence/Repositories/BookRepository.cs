using Domain.Common.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<Book>> GetByGenreAsync(string genre)
    {
        return await DbSet.Where(x => x.Genre == genre).ToListAsync();
    }
}