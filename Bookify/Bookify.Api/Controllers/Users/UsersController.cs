using Bookify.Application.Users;
using Bookify.Application.Users.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Users;


[ApiController]
[Route("api/[controller]")]
public class UsersController(ISender sender) : ControllerBase
{
    private readonly ISender sender = sender;

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(request.Email, request.Password, request.FirstName, request.LastName);
        Domain.Abstractions.Result<Guid> result = await sender.Send(command, cancellationToken);

        return Ok(result.Value);
    }


    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(request.Email, request.Password);
        Domain.Abstractions.Result<AccessTokenResponse> result = await sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return Unauthorized(result.Error);
        }
        return Ok(result.Value);
    }
}
