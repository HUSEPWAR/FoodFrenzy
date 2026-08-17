using FoodFrenzy.Application.Users.Registration;
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
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _registrationService.RegisterAsync(
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
            user.CreatedAt
        });
    }
}
