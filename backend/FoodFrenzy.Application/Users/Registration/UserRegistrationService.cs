
using System.Net.Mail;
using FoodFrenzy.Application.Security;
using FoodFrenzy.Domain.Entities;

namespace FoodFrenzy.Application.Users.Registration;

public sealed class UserRegistrationService : IUserRegistrationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserRegistrationService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> RegisterAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        // First name validation
        if (string.IsNullOrWhiteSpace(request.FirstName))
            throw new ArgumentException("First name is required.");

        if (request.FirstName.Trim().Length > 100)
            throw new ArgumentException(
                "First name cannot exceed 100 characters.");

        // Last name validation
        if (string.IsNullOrWhiteSpace(request.LastName))
            throw new ArgumentException("Last name is required.");

        if (request.LastName.Trim().Length > 100)
            throw new ArgumentException(
                "Last name cannot exceed 100 characters.");

        // Email validation
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException("Email is required.");

        var email = request.Email.Trim();

        if (email.Length > 255)
            throw new ArgumentException(
                "Email cannot exceed 255 characters.");

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

        if (request.Password.Length < 8)
            throw new ArgumentException(
                "Password must be at least 8 characters long.");

        if (request.Password.Length > 128)
            throw new ArgumentException(
                "Password cannot exceed 128 characters.");

        // Phone validation
        if (!string.IsNullOrWhiteSpace(request.PhoneNumber) &&
            request.PhoneNumber.Trim().Length > 20)
        {
            throw new ArgumentException(
                "Phone number cannot exceed 20 characters.");
        }

        // Normalize email
        var normalizedEmail = email.ToUpperInvariant();

        // Check duplicate email
        var existingUser =
            await _userRepository.GetByNormalizedEmailAsync(
                normalizedEmail,
                cancellationToken);

        if (existingUser is not null)
        {
            throw new InvalidOperationException(
                "A user with this email already exists.");
        }

        // Hash password
        var passwordHash = _passwordHasher.Hash(request.Password);

        // Create user
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = email,
            NormalizedEmail = normalizedEmail,
            PasswordHash = passwordHash,
            PhoneNumber = request.PhoneNumber?.Trim() ?? string.Empty,
            IsEmailVerified = false,
            IsActive = true,
            FailedLoginAttempts = 0,
            CreatedAt = DateTime.UtcNow
        };

        // Persist user
        await _userRepository.AddAsync(
            user,
            cancellationToken);

        await _userRepository.SaveChangesAsync(
            cancellationToken);

        return user;
    }
}

