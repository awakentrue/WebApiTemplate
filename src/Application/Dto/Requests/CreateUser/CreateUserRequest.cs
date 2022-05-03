namespace Application.Dto.Requests.CreateUser;

public class CreateUserRequest
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}