using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Exceptions;

namespace WebApi.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var modelState = context.ModelState;
        if (modelState.IsValid == false)
        {
            var errors = modelState.Values.Where(x => x.Errors.Any())
                                          .SelectMany(x => x.Errors)
                                          .Select(x => x.ErrorMessage)
                                          .ToList();
            
            throw new ValidationException(errors);
        }

        await next();
    }
}