using System.Data;
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
    public ActionResponse AddUser(
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

            var result = new ActionResponse()
            {
                Success = errorCode == 0,
                Message = errorMessage,
                PossibleFix = errorCode switch
                {
                    400 => "Please check all required fields are filled",
                    409 => "Try a different username or email",
                    _ => "Contact system administrator"
                }
            };
        }
        catch (Exception e)
        {
            return new ActionResponse()
            {
                Success = false,
                Message = e.Message,
                PossibleFix = "Check database connection or permission"
            };
        }

        return new ActionResponse()
        {
            Success = true, Message = "User added successfully."
        };
    }

}