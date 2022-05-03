using Application.Contracts.Authorization;
using Application.Dto.Requests.CreateUser;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class AccountController : ApiControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
    }

    [HttpPost]
    public async Task<Response> CreateUser([FromBody] CreateUserRequest createUserRequest)
    {
        return await _accountService.CreateUserAsync(createUserRequest.UserName, createUserRequest.Password);
    }
}