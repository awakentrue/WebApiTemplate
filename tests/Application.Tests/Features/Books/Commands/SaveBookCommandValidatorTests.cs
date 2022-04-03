using System.Threading.Tasks;
using Application.Features.Books.Commands.SaveBook;
using FluentValidation.TestHelper;
using Xunit;

namespace Application.Tests.Features.Books.Commands;

public class SaveBookCommandValidatorTests
{
    private readonly SaveBookCommandValidator _validator;
    
    public SaveBookCommandValidatorTests()
    {
        _validator = new SaveBookCommandValidator();
    }

    [Fact]
    public async Task Validate_ShouldHaveValidationErrors_WhenTitleAndGenreAreNull()
    {
        var command = new SaveBookCommand();
        var result = await _validator.TestValidateAsync(command);
        
        result.ShouldHaveValidationErrorFor(book => book.Title);
        result.ShouldHaveValidationErrorFor(book => book.Genre);
    }
}