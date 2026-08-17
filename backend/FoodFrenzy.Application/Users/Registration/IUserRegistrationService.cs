using FoodFrenzy.Domain.Entities;

namespace FoodFrenzy.Application.Users.Registration;

public interface IUserRegistrationService
{
    Task<User> RegisterAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default);


}