using Application.Wrappers;
using Domain.Common;
using Domain.Common.Repositories;
using Domain.Entities;
using MapsterMapper;

namespace Application.Features.Books.Commands.SaveBook;

public class SaveBookCommandHandler : IRequestHandler<SaveBookCommand, int>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SaveBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Response<int>> Handle(SaveBookCommand request, CancellationToken cancellationToken)
    {
        var book = _mapper.Map<Book>(request);
        await _bookRepository.SaveAsync(book, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Response.Success(book.Id);
    }
}