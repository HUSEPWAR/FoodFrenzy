using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodFrenzy.Application.Users.Registration;
using FoodFrenzy.Application.Users.Login;

namespace FoodFrenzy.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserRegistrationService _registrationService;
    private readonly IUserLoginService _loginService;

    public UsersController(
        IUserRegistrationService registrationService,
        IUserLoginService loginService)
    {
        _registrationService = registrationService;
        _loginService = loginService;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _registrationService.RegisterAsync(
            request,
            cancellationToken);

        var response = new RegisterUserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            IsEmailVerified = user.IsEmailVerified,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };

        return CreatedAtAction(
            nameof(Register),
            new { id = user.Id },
            response);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(
    [FromBody] LoginUserRequest request,
    CancellationToken cancellationToken)
    {
        var user = await _loginService.LoginAsync(
            request,
            cancellationToken);

        return Ok(new
        {
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.PhoneNumber,
            user.IsEmailVerified,
            user.IsActive,
            user.LastLoginAt
        });
    }
}
