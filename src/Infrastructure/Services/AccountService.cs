using Application.Contracts.Authorization;
using Application.Wrappers;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager;

    public AccountService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<Response> CreateUserAsync(string userName, string password)
    {
        var user = new IdentityUser(userName);

        var result = await _userManager.CreateAsync(user, password);

        var response = result.Succeeded
            ? Response.Success()
            : Response.Fail(result.Errors.Select(x => new ResponseError(x.Description)));

        return response;
    }
}