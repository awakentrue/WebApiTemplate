using Application.Dto;
using Application.Wrappers;
using Domain.Common.Repositories;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace Application.Features.Books.Queries.GetBooks;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetAllBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Response<IEnumerable<BookDto>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync(cancellationToken);
        var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);

        return Response.Success(booksDto);
    }
}