using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstatePlatform.API.Contracts;
using Users.Application;

namespace RealEstatePlatform.API.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request, CancellationToken cancellationToken)
        => Ok(await sender.Send(new RegisterUserCommand(request.FullName, request.Email, request.PhoneNumber, request.Password, request.Roles), cancellationToken));
}
