using System.Threading;
using System.Threading.Tasks;
using Application.Features.Books.Commands.SaveBook;
using Domain.Common;
using Domain.Common.Repositories;
using Domain.Entities;
using FluentAssertions;
using GenerationRules.Rules;
using MapsterMapper;
using Moq;
using Xunit;

namespace Application.Tests.Features.Books.Commands;

public class SaveBookCommandHandlerTests
{
    private readonly BookGenerationRule _bookGenerationRule;
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly SaveBookCommandHandler _handler;

    public SaveBookCommandHandlerTests(BookGenerationRule bookGenerationRule)
    {
        _bookGenerationRule = bookGenerationRule;
        _bookRepositoryMock = new Mock<IBookRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new SaveBookCommandHandler(_bookRepositoryMock.Object, _unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task SaveBook_ShouldSaveBookAndReturnId()
    {
        var book = _bookGenerationRule.RuleFor(x => x.Id, x => x.Random.Number(1000)).Generate();
        
        _mapperMock.Setup(x => x.Map<Book>(It.IsAny<SaveBookCommand>())).Returns(book);
        
        var command = new SaveBookCommand
        {
            Title = book.Title,
            Genre = book.Genre
        };

        var bookIdResponse = await _handler.Handle(command, CancellationToken.None);

        bookIdResponse.IsSuccessful.Should().BeTrue();
        bookIdResponse.Errors.Should().BeNull();
        bookIdResponse.Data.Should().Be(book.Id);
    }
}