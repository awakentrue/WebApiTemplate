using Application.Dto;
using Application.Wrappers;
using Domain.Common.Repositories;
using MapsterMapper;

namespace Application.Features.Books.Queries.GetBook;

public class GetBookQueryHandler: IRequestHandler<GetBookQuery, BookDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetBookQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Response<BookDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetAsync(request.Id, cancellationToken);
        var bookDto = _mapper.Map<BookDto>(book);
        
        return Response.Success(bookDto);
    }
}