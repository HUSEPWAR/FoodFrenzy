using FoodFrenzy.Infrastructure.Security;

namespace FoodFrenzy.Tests;

public class Argon2PasswordHasherTests
{
    [Fact]
    public void Hash_ShouldCreateDifferentHashForSamePassword()
    {
        var hasher = new Argon2PasswordHasher();

        var password = "TestPassword123!";

        var hash1 = hasher.Hash(password);
        var hash2 = hasher.Hash(password);

        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void Verify_ShouldReturnTrueForCorrectPassword()
    {
        var hasher = new Argon2PasswordHasher();

        var password = "TestPassword123!";
        var hash = hasher.Hash(password);

        var result = hasher.Verify(password, hash);

        Assert.True(result);
    }

    [Fact]
    public void Verify_ShouldReturnFalseForIncorrectPassword()
    {
        var hasher = new Argon2PasswordHasher();

        var password = "TestPassword123!";
        var wrongPassword = "WrongPassword123!";
        var hash = hasher.Hash(password);

        var result = hasher.Verify(wrongPassword, hash);

        Assert.False(result);
    }
}