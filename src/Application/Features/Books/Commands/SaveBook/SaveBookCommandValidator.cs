using FluentValidation;

namespace Application.Features.Books.Commands.SaveBook;

public class SaveBookCommandValidator : AbstractValidator<SaveBookCommand>
{
    public SaveBookCommandValidator()
    {
        RuleFor(x => x.Genre)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty();
    }
}