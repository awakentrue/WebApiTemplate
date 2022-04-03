using Application.Dto;
using Application.Wrappers;

namespace Application.Features.Books.Queries.GetBooks;

public class GetAllBooksQuery : IRequest<IEnumerable<BookDto>>
{
}