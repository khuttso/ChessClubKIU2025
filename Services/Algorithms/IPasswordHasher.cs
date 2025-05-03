namespace ChessClubKIU.Services.Algorithms;

public interface IPasswordHasher
{
    (byte[] hash, byte[] salt) HashPassword(string password);
    bool VerifyHashedPassword(string hashedPassword, byte[] storedHash, byte[] storedSalt);
}   