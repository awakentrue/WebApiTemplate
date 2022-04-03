using System.Net.Mime;
using System.Text.Json;
using Application.Exceptions;
using Application.Wrappers;
using Domain.Common.Exceptions;
using FluentValidation;

namespace WebApi.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;
            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = MediaTypeNames.Application.Json;
            
            var errors = exception switch
            {
                ApiException e => ApiExceptionHandler(e),
                EntityNotFoundException e => EntityNotFoundExceptionHandler(e),
                ValidationException e => ValidationExceptionHandler(e),
                _ => UnknownExceptionHandler(exception)
            };
            
            var failResponse = Response.Fail<string>(errors);
            var serializedResponse = JsonSerializer.Serialize(failResponse);
            
            await response.WriteAsync(serializedResponse);
        }
    }

    private IEnumerable<ResponseError> ApiExceptionHandler(ApiException exception)
    {
        yield return ResponseError.New(exception.Message);
    }

    private IEnumerable<ResponseError> EntityNotFoundExceptionHandler(EntityNotFoundException exception)
    {
        yield return ResponseError.New(exception.Message);
    }

    private IEnumerable<ResponseError> ValidationExceptionHandler(ValidationException exception)
    {
        return exception.Errors.Select(x => ResponseError.New(x.ErrorMessage));
    }

    private IEnumerable<ResponseError> UnknownExceptionHandler(Exception exception)
    {
        const string unknownErrorMessage = "Unknown error";
        _logger.LogError(exception, unknownErrorMessage);
        
        yield return ResponseError.New(ResponseErrors.Common.InternalServerError);
    }
}