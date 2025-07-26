using System;
using ChessClubKIU.DAOs.Users;
using ChessClubKIU.RequestResponse;

namespace ChessClubKIU.DbManagers.Templates;

public interface IUserManagementDbManager
{
    ActionResponse<int> AddUser(string username, string email, string gender, byte[] passwordHash, byte[] passwordSalt, string? refreshToken = null, DateTime? refreshTokenExpiry = null);
    ActionResponse<User> GetUserByCredential(string usernameOrEmail);
    ActionResponse<int> UpdateRefreshToken(int userId, string refreshToken, DateTime expiry);
}       