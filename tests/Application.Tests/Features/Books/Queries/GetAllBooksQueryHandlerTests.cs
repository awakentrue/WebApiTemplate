using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto;
using Application.Features.Books.Queries.GetBooks;
using Domain.Common.Repositories;
using FluentAssertions;
using GenerationRules.Rules;
using MapsterMapper;
using Moq;
using Xunit;

namespace Application.Tests.Features.Books.Queries;

public class GetAllBooksQueryHandlerTests
{
    private readonly BookGenerationRule _bookGenerationRule;
    private readonly IMapper _mapper;
    private readonly GetAllBooksQueryHandler _handler;
    private readonly Mock<IBookRepository> _bookRepositoryMock;

    public GetAllBooksQueryHandlerTests(BookGenerationRule bookGenerationRule, IMapper mapper)
    {
        _bookGenerationRule = bookGenerationRule;
        _mapper = mapper;
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new GetAllBooksQueryHandler(_bookRepositoryMock.Object, mapper);
    }

    [Fact]
    public async Task GetAllBooks_ShouldReturnBooks_WhenBooksExistsInDatabase()
    {
        var booksFromDatabase = _bookGenerationRule.GenerateLazy(5).ToList();
        _bookRepositoryMock.Setup(x => x.GetAllAsync(CancellationToken.None)).ReturnsAsync(booksFromDatabase);
        
        var query = new GetAllBooksQuery();
        var books = await _handler.Handle(query, CancellationToken.None);

        books.Data.Should().BeEquivalentTo(_mapper.Map<IEnumerable<BookDto>>(booksFromDatabase));
    }
}