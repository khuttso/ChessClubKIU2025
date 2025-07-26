using ChessClubKIU.DAOs.Users;

namespace ChessClubKIU.Services.Templates;

public interface IJwtTokenService
{
    string GenerateJwtToken(int userId, string userName);
}   