namespace ChessClubKIU.Services.Templates;

public interface IJwtTokenService
{
    string GenerateToken(int userId, string username);
}
