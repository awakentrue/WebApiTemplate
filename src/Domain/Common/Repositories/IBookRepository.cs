using Domain.Entities;

namespace Domain.Common.Repositories;

public interface IBookRepository : IRepository<Book>
{
    Task<IReadOnlyCollection<Book>> GetByGenreAsync(string genre);
}