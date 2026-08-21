using FoodFrenzy.Application.Security;
using FoodFrenzy.Application.Users.Login;
using FoodFrenzy.Application.Users;
using FoodFrenzy.Domain.Entities;

namespace FoodFrenzy.Tests;

public sealed class UserLoginServiceTests
{
    [Fact]
    public async Task LoginAsync_ShouldReturnUser_WhenCredentialsAreValid()
    {
        var repository = new FakeUserRepository();
        var passwordHasher = new FakePasswordHasher();

        var user = CreateUser();
        user.Email = "test@example.com";
        user.NormalizedEmail = "TEST@EXAMPLE.COM";
        user.PasswordHash = "HASHED_TestPassword123!";

        repository.Users.Add(user);

        var service = new UserLoginService(
            repository,
            passwordHasher);

        var request = new LoginUserRequest
        {
            Email = "test@example.com",
            Password = "TestPassword123!"
        };

        var result = await service.LoginAsync(request);

        Assert.Equal(user.Id, result.Id);
        Assert.Equal("test@example.com", result.Email);
        Assert.True(result.IsActive);
        Assert.NotNull(result.LastLoginAt);
        Assert.Equal(0, result.FailedLoginAttempts);
    }

    [Fact]
    public async Task LoginAsync_ShouldRejectEmptyEmail()
    {
        var service = CreateService();

        var request = CreateValidRequest();
        request.Email = "";

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.LoginAsync(request));

        Assert.Equal(
            "Email is required.",
            exception.Message);
    }

    [Fact]
    public async Task LoginAsync_ShouldRejectInvalidEmail()
    {
        var service = CreateService();

        var request = CreateValidRequest();
        request.Email = "invalid-email";

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.LoginAsync(request));

        Assert.Equal(
            "A valid email address is required.",
            exception.Message);
    }

    [Fact]
    public async Task LoginAsync_ShouldRejectEmptyPassword()
    {
        var service = CreateService();

        var request = CreateValidRequest();
        request.Password = "";

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.LoginAsync(request));

        Assert.Equal(
            "Password is required.",
            exception.Message);
    }

    [Fact]
    public async Task LoginAsync_ShouldRejectUserNotFound()
    {
        var service = CreateService();

        var request = CreateValidRequest();

        var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => service.LoginAsync(request));

        Assert.Equal(
            "Invalid email or password.",
            exception.Message);
    }

    [Fact]
    public async Task LoginAsync_ShouldRejectWrongPassword()
    {
        var repository = new FakeUserRepository();
        var passwordHasher = new FakePasswordHasher();

        var user = CreateUser();
        user.Email = "test@example.com";
        user.NormalizedEmail = "TEST@EXAMPLE.COM";
        user.PasswordHash = "HASHED_CorrectPassword123!";

        repository.Users.Add(user);

        var service = new UserLoginService(
            repository,
            passwordHasher);

        var request = new LoginUserRequest
        {
            Email = "test@example.com",
            Password = "WrongPassword123!"
        };

        var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => service.LoginAsync(request));

        Assert.Equal(
            "Invalid email or password.",
            exception.Message);
    }

    [Fact]
    public async Task LoginAsync_ShouldRejectInactiveUser()
    {
        var repository = new FakeUserRepository();
        var passwordHasher = new FakePasswordHasher();

        var user = CreateUser();
        user.Email = "inactive@example.com";
        user.NormalizedEmail = "INACTIVE@EXAMPLE.COM";
        user.PasswordHash = "HASHED_TestPassword123!";
        user.IsActive = false;

        repository.Users.Add(user);

        var service = new UserLoginService(
            repository,
            passwordHasher);

        var request = new LoginUserRequest
        {
            Email = "inactive@example.com",
            Password = "TestPassword123!"
        };

        var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => service.LoginAsync(request));

        Assert.Equal(
            "Invalid email or password.",
            exception.Message);
    }

    [Fact]
    public async Task LoginAsync_ShouldResetFailedAttempts_WhenLoginSucceeds()
    {
        var repository = new FakeUserRepository();
        var passwordHasher = new FakePasswordHasher();

        var user = CreateUser();
        user.Email = "test@example.com";
        user.NormalizedEmail = "TEST@EXAMPLE.COM";
        user.PasswordHash = "HASHED_TestPassword123!";
        user.FailedLoginAttempts = 5;

        repository.Users.Add(user);

        var service = new UserLoginService(
            repository,
            passwordHasher);

        var request = new LoginUserRequest
        {
            Email = "test@example.com",
            Password = "TestPassword123!"
        };

        await service.LoginAsync(request);

        Assert.Equal(0, user.FailedLoginAttempts);
    }

    [Fact]
    public async Task LoginAsync_ShouldUpdateLastLoginAt_WhenLoginSucceeds()
    {
        var repository = new FakeUserRepository();
        var passwordHasher = new FakePasswordHasher();

        var user = CreateUser();
        user.Email = "test@example.com";
        user.NormalizedEmail = "TEST@EXAMPLE.COM";
        user.PasswordHash = "HASHED_TestPassword123!";
        user.LastLoginAt = null;

        repository.Users.Add(user);

        var service = new UserLoginService(
            repository,
            passwordHasher);

        var request = new LoginUserRequest
        {
            Email = "test@example.com",
            Password = "TestPassword123!"
        };

        var before = DateTime.UtcNow;

        await service.LoginAsync(request);

        var after = DateTime.UtcNow;

        Assert.NotNull(user.LastLoginAt);
        Assert.InRange(
            user.LastLoginAt.Value,
            before,
            after);
    }

    private static UserLoginService CreateService()
    {
        return new UserLoginService(
            new FakeUserRepository(),
            new FakePasswordHasher());
    }

    private static LoginUserRequest CreateValidRequest()
    {
        return new LoginUserRequest
        {
            Email = "test@example.com",
            Password = "TestPassword123!"
        };
    }

    private static User CreateUser()
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "User",
            Email = "test@example.com",
            NormalizedEmail = "TEST@EXAMPLE.COM",
            PasswordHash = "HASHED_TestPassword123!",
            PhoneNumber = "9999999999",
            IsEmailVerified = false,
            IsActive = true,
            FailedLoginAttempts = 0,
            CreatedAt = DateTime.UtcNow
        };
    }

    private sealed class FakeUserRepository : IUserRepository
    {
        public List<User> Users { get; } = new();

        public Task<User?> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(
                Users.FirstOrDefault(
                    user => user.Email == email));
        }

        public Task<User?> GetByNormalizedEmailAsync(
            string normalizedEmail,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(
                Users.FirstOrDefault(
                    user => user.NormalizedEmail == normalizedEmail));
        }

        public Task<User?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(
                Users.FirstOrDefault(
                    user => user.Id == id));
        }

        public Task AddAsync(
            User user,
            CancellationToken cancellationToken = default)
        {
            Users.Add(user);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }

    private sealed class FakePasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return $"HASHED_{password}";
        }

        public bool Verify(
            string password,
            string passwordHash)
        {
            return passwordHash == $"HASHED_{password}";
        }
    }
}