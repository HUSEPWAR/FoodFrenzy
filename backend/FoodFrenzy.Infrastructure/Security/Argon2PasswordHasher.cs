using System.Security.Cryptography;
using System.Text;
using FoodFrenzy.Application.Security;
using Konscious.Security.Cryptography;

namespace FoodFrenzy.Infrastructure.Security;

public sealed class Argon2PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;

    private const int Iterations = 3;
    private const int MemorySize = 65536;
    private const int DegreeOfParallelism = 4;

    public string Hash(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        byte[] hash = HashPassword(
            password,
            salt,
            Iterations,
            MemorySize,
            DegreeOfParallelism);

        return $"{Iterations}.{MemorySize}.{DegreeOfParallelism}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    public bool Verify(string password, string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);

        string[] parts = passwordHash.Split('.');

        if (parts.Length != 5)
            return false;

        if (!int.TryParse(parts[0], out int iterations) ||
            !int.TryParse(parts[1], out int memorySize) ||
            !int.TryParse(parts[2], out int degreeOfParallelism))
        {
            return false;
        }

        byte[] salt;
        byte[] expectedHash;

        try
        {
            salt = Convert.FromBase64String(parts[3]);
            expectedHash = Convert.FromBase64String(parts[4]);
        }
        catch (FormatException)
        {
            return false;
        }

        byte[] actualHash = HashPassword(
            password,
            salt,
            iterations,
            memorySize,
            degreeOfParallelism);

        return CryptographicOperations.FixedTimeEquals(
            actualHash,
            expectedHash);
    }

    private static byte[] HashPassword(
        string password,
        byte[] salt,
        int iterations,
        int memorySize,
        int degreeOfParallelism)
    {
        var argon2 = new Argon2id(
            Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            Iterations = iterations,
            MemorySize = memorySize,
            DegreeOfParallelism = degreeOfParallelism
        };

        return argon2.GetBytes(HashSize);
    }
}