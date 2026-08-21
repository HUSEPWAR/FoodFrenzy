using System.Net.Mail;
using FoodFrenzy.Application.Security;
using FoodFrenzy.Domain.Entities;

namespace FoodFrenzy.Application.Users.Login;

public sealed class UserLoginService : IUserLoginService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserLoginService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> LoginAsync(
        LoginUserRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Email validation
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException("Email is required.");

        var email = request.Email.Trim();

        if (!MailAddress.TryCreate(email, out var mailAddress) ||
            !string.Equals(
                mailAddress.Address,
                email,
                StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException(
                "A valid email address is required.");
        }

        // Password validation
        if (string.IsNullOrWhiteSpace(request.Password))
            throw new ArgumentException("Password is required.");

        // Normalize email
        var normalizedEmail = email.ToUpperInvariant();

        // Find user
        var user = await _userRepository.GetByNormalizedEmailAsync(
            normalizedEmail,
            cancellationToken);

        // Do not reveal whether the email exists
        if (user is null)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        // Check account status
        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        // Verify password
        var passwordValid = _passwordHasher.Verify(
            request.Password,
            user.PasswordHash);

        if (!passwordValid)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        // Successful login
        user.FailedLoginAttempts = 0;
        user.LastLoginAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.SaveChangesAsync(
            cancellationToken);

        return user;
    }
}