using FoodFrenzy.Domain.Entities;

namespace FoodFrenzy.Application.Users.Login;

public interface IUserLoginService
{
    Task<User> LoginAsync(
        LoginUserRequest request,
        CancellationToken cancellationToken = default);
}