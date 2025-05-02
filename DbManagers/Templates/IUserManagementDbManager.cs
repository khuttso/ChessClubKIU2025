using ChessClubKIU.DTOs.Users;
using ChessClubKIU.RequestResponse;

namespace ChessClubKIU.DbManagers.Templates;

public interface IUserManagementDbManager
{
    ActionResponse AddUser(string username, string email, string gender, byte[] passwordHash, byte[] passwordSalt, string? refreshToken = null, DateTime? refreshTokenExpiry = null); 
}   