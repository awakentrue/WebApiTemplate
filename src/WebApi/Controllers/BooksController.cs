using Application.Dto;
using Application.Features.Books.Commands.SaveBook;
using Application.Features.Books.Queries.GetBook;
using Application.Features.Books.Queries.GetBooks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BooksResponse = Application.Wrappers.Response<System.Collections.Generic.IEnumerable<Application.Dto.BookDto>>;

namespace WebApi.Controllers;

[Authorize]
public class BooksController : ApiControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookDto>> GetBook(int id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetBookQuery(id), cancellationToken));
    }
    
    [HttpGet]
    public async Task<ActionResult<BooksResponse>> GetAllBooks(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAllBooksQuery(), cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<int>> SaveBook([FromBody] SaveBookCommand saveBookCommand, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(saveBookCommand, cancellationToken));
    }
}