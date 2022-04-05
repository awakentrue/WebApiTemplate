using Application.Wrappers;

namespace Application.Features.Books.Commands.SaveBook;

public class SaveBookCommand : IRequest<int>
{
    public string? Title { get; set; }
    
    public string? Genre { get; set; }
}