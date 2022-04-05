using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Repositories;
using Domain.Entities;
using FluentAssertions;
using GenerationRules.Rules;
using Infrastructure.Persistence.Repositories;
using Xunit;

namespace Infrastructure.Tests.Repositories;

public class BookRepositoryTests : RepositoryTests<Book>
{
    private readonly BookGenerationRule _bookGenerationRule;
    private readonly IBookRepository _bookRepository;

    public BookRepositoryTests(BookGenerationRule bookBookGenerationRule) : base(bookBookGenerationRule)
    {
        _bookGenerationRule = bookBookGenerationRule;
        _bookRepository = new BookRepository(Context);
    }

    [Fact]
    public async Task GetByGenreAsync_ShouldReturnAllBooksByGenre_IfSuchBooksExist()
    {
        var books = _bookGenerationRule.GenerateLazy(5).ToArray();
        await SaveEntitiesAsync(books);

        var firstBook = books.First();

        var foundBook = await _bookRepository.GetByGenreAsync(firstBook.Genre!);
        foundBook.Should().BeEquivalentTo(new[] {firstBook});
    }

    [Fact]
    public async Task Update_ShouldUpdateBookProperties()
    {
        var book = _bookGenerationRule.Generate();
        await SaveEntitiesAsync(book);

        var newBook = _bookGenerationRule.Generate();
        book.Title = newBook.Title;
        book.Genre = newBook.Genre;
        
        _bookRepository.Update(book);
        await UnitOfWork.CommitAsync();

        book = await GetEntity<Book>(book.Id);
        
        book.Title.Should().Be(newBook.Title);
        book.Genre.Should().Be(newBook.Genre);
    }
}