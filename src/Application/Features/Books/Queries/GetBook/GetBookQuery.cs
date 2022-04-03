using Application.Dto;
using Application.Wrappers;

namespace Application.Features.Books.Queries.GetBook;

public class GetBookQuery : IRequest<BookDto>
{
    public GetBookQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}