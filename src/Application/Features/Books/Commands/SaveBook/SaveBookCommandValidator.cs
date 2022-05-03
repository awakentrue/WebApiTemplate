using FluentValidation;

namespace Application.Features.Books.Commands.SaveBook;

public class SaveBookCommandValidator : AbstractValidator<SaveBookCommand>
{
    public SaveBookCommandValidator()
    {
        RuleFor(x => x.Genre)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Title)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();
    }
}