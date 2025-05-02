using ChessClubKIU.DAOs.Users;
using ChessClubKIU.DTOs.Users;

namespace ChessClubKIU.Mappers;

public class UserMapper
{
    public static User FromRegistrationDto(RegisterUserRequest dto, byte[] hash, byte[] salt)
    {
        return new User
        {
            Username = dto.Username,
            Email = dto.Email,
            Gender = dto.Gender,
            PasswordHash = hash,
            PasswordSalt = salt,
            CreatedAt = DateTime.UtcNow
        };
    } 
}