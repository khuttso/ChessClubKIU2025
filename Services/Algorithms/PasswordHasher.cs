using System.Security.Cryptography;
using System.Text;

namespace ChessClubKIU.Services.Algorithms;

public class PasswordHasher : IPasswordHasher
{
    public (byte[] hash, byte[] salt) HashPassword(string password)
    {
        using var hmac = new HMACSHA512();
        return (
            hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            hmac.Key
        );
    }

    public bool VerifyHashedPassword(string hashedPassword, byte[] storedHash, byte[] storedSalt)
    {
        using var hmac = new HMACSHA512(storedSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(hashedPassword));
        return computedHash.SequenceEqual(storedHash);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}