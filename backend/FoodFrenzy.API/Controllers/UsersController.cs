using FoodFrenzy.Application.Users.Registration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodFrenzy.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserRegistrationService _registrationService;

    public UsersController(
        IUserRegistrationService registrationService)
    {
        _registrationService = registrationService;
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
}