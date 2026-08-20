namespace FoodFrenzy.Application.Users.Registration;

public sealed class RegisterUserResponse
{
    public Guid Id { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string PhoneNumber { get; init; } = string.Empty;

    public bool IsEmailVerified { get; init; }

    public bool IsActive { get; init; }

    public DateTime CreatedAt { get; init; }
}