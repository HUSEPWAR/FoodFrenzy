using FoodFrenzy.Application.Security;
using FoodFrenzy.Application.Users;
using FoodFrenzy.Application.Users.Registration;
using FoodFrenzy.Domain.Entities;

namespace FoodFrenzy.Tests;

public sealed class UserRegistrationServiceTests
{
    [Fact]
    public async Task RegisterAsync_ShouldCreateUser_WhenRequestIsValid()
    {
        var repository = new FakeUserRepository();
        var passwordHasher = new FakePasswordHasher();

        var service = new UserRegistrationService(
            repository,
            passwordHasher);

        var request = new RegisterUserRequest
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test@example.com",
            Password = "TestPassword123!",
            PhoneNumber = "9999999999"
        };

        var user = await service.RegisterAsync(request);

        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal("Test", user.FirstName);
        Assert.Equal("User", user.LastName);
        Assert.Equal("test@example.com", user.Email);
        Assert.Equal("TEST@EXAMPLE.COM", user.NormalizedEmail);
        Assert.Equal("HASHED_TestPassword123!", user.PasswordHash);
        Assert.True(user.IsActive);

        Assert.Single(repository.Users);
    }

    [Fact]
    public async Task RegisterAsync_ShouldRejectEmptyFirstName()
    {
        var service = CreateService();

        var request = CreateValidRequest();
        request.FirstName = "";

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.RegisterAsync(request));

        Assert.Equal("First name is required.", exception.Message);
    }

    [Fact]
    public async Task RegisterAsync_ShouldRejectEmptyLastName()
    {
        var service = CreateService();

        var request = CreateValidRequest();
        request.LastName = "";

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.RegisterAsync(request));

        Assert.Equal("Last name is required.", exception.Message);
    }

    [Fact]
    public async Task RegisterAsync_ShouldRejectInvalidEmail()
    {
        var service = CreateService();

        var request = CreateValidRequest();
        request.Email = "invalid-email";

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.RegisterAsync(request));

        Assert.Equal(
            "A valid email address is required.",
            exception.Message);
    }

    [Fact]
    public async Task RegisterAsync_ShouldRejectShortPassword()
    {
        var service = CreateService();

        var request = CreateValidRequest();
        request.Password = "1234567";

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.RegisterAsync(request));

        Assert.Equal(
            "Password must be at least 8 characters long.",
            exception.Message);
    }

    [Fact]
    public async Task RegisterAsync_ShouldRejectLongPassword()
    {
        var service = CreateService();

        var request = CreateValidRequest();
        request.Password = new string('A', 129);

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.RegisterAsync(request));

        Assert.Equal(
            "Password cannot exceed 128 characters.",
            exception.Message);
    }

    [Fact]
    public async Task RegisterAsync_ShouldRejectDuplicateEmail()
    {
        var repository = new FakeUserRepository();
        var passwordHasher = new FakePasswordHasher();

        var existingUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Existing",
            LastName = "User",
            Email = "existing@example.com",
            NormalizedEmail = "EXISTING@EXAMPLE.COM",
            PasswordHash = "existing-hash",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        repository.Users.Add(existingUser);

        var service = new UserRegistrationService(
            repository,
            passwordHasher);

        var request = CreateValidRequest();
        request.Email = " existing@example.com ";

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.RegisterAsync(request));

        Assert.Equal(
            "A user with this email already exists.",
            exception.Message);

        Assert.Single(repository.Users);
    }

    private static UserRegistrationService CreateService()
    {
        return new UserRegistrationService(
            new FakeUserRepository(),
            new FakePasswordHasher());
    }

    private static RegisterUserRequest CreateValidRequest()
    {
        return new RegisterUserRequest
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test@example.com",
            Password = "TestPassword123!",
            PhoneNumber = "9999999999"
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

