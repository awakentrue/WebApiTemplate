using Application.Wrappers;

namespace Application.Contracts.Authorization;

public interface IAccountService
{
    Task<Response> CreateUserAsync(string userName, string password);
}