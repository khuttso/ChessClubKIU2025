using System.Data;
using ChessClubKIU.DAOs.Users;
using ChessClubKIU.DbManagers.Templates;
using ChessClubKIU.DTOs.Users;
using ChessClubKIU.RequestResponse;
using Dapper;
using MySqlConnector;

namespace ChessClubKIU.DbManagers.MySQL;

public class UserManagementDbManager : IUserManagementDbManager
{
    private readonly MySqlConnection _connection;

    public UserManagementDbManager(MySqlConnection connection)
    {
        _connection = connection;
    }
    public ActionResponse<int> AddUser(
    string username, 
    string email,
    string gender,
    byte[] passwordHash,
    byte[] passwordSalt,
    string? refreshToken = null,
    DateTime? refreshTokenExpiry = null
    )
    {
        try
        {
            var parameters = new DynamicParameters(
                new
                {
                    Username = username,
                    Email = email,
                    Gender = gender,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiry = refreshTokenExpiry
                }
            );

            parameters.Add("@ErrorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output);
            var cmd = new CommandDefinition(
                "security_AddUser",
                parameters,
                commandType: CommandType.StoredProcedure,
                commandTimeout: 30
            );

            _connection.Execute(cmd);

            var errorCode = parameters.Get<int>("@ErrorCode");
            var errorMessage = parameters.Get<string>("@ErrorMessage");

            var result = new ActionResponse<int>()
            {
                Success = errorCode == 0,
                Message = errorMessage,
                PossibleFix = errorCode switch
                {
                    400 => "Please check all required fields are filled",
                    409 => "Try a different username or email",
                    _ => "If anything is not fine contact system administrator"
                }
            };
            
            return result;
        }
        catch (Exception e)
        {
            return new ActionResponse<int>()
            {
                Success = false,
                Message = e.Message,
                PossibleFix = "Check database connection or permission"
            };
        }
    }

    public ActionResponse<User> GetUserByCredential(string usernameOrEmail)
    {
        try
        {
            var parameters = new DynamicParameters(new { UsernameOrEmail = usernameOrEmail });

            var cmd = new CommandDefinition(
                "security_GetUserByCredential",
                parameters,
                commandType: CommandType.StoredProcedure,
                commandTimeout: 30
            );
            var user = _connection.QueryFirstOrDefault<User>(cmd);

            var result = new ActionResponse<User>()
            {
                Success = true,
                Message = user == null ? "User has not been found" : "User found successfully",
                Data = user
            };
            
            return result;
        }
        catch (Exception e)
        {
            return new ActionResponse<User>()
            {
                Success = false,
                Message = e.Message,
                PossibleFix = "Check database connection or permission"
            };
        }
    }

    public ActionResponse<int> UpdateRefreshToken(int userId, string refreshToken, DateTime expiry)
    {
        try
        {
            var parameters = new DynamicParameters();

            parameters.Add("P_UserId", userId);
            parameters.Add("P_RefreshToken", refreshToken);
            parameters.Add("P_Expiry", expiry);

            parameters.Add("ErrorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("ErrorMessage", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            
            var cmd = new CommandDefinition(
                "security_UpdateRefreshToken",
                parameters,
                commandType: CommandType.StoredProcedure,
                commandTimeout: 30
            );

            _connection.Execute(cmd);
            
            var errorCode = parameters.Get<int>("@ErrorCode");
            var errorMessage = parameters.Get<string>("@ErrorMessage");

            return new ActionResponse<int>()
            {
                Success = errorCode == 0,
                Message = errorMessage,
                PossibleFix = errorCode switch
                {
                    400 => "Please check all required fields are filled",
                    408 => "Field is empty, please try again",
                    _ => "If anything is not fine contact system administrator"
                }
            };
        }
        catch (Exception e)
        {
            return new ActionResponse<int>()
            {
                Success = false,
                Message = e.Message,
                PossibleFix = "Check database connection or permission"
            };
        }
    }
}