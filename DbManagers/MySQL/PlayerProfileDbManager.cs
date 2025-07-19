using System.Data;
using ChessClubKIU.DAOs.Users;
using ChessClubKIU.DbManagers.Templates;
using ChessClubKIU.RequestResponse;
using Dapper;
using MySqlConnector;

namespace ChessClubKIU.DbManagers.MySQL;

public class PlayerProfileDbManager : IPlayerProfileDbManager
{
    private readonly MySqlConnection _connection;

    public PlayerProfileDbManager(MySqlConnection connection)
    {
        _connection = connection;
    }
    
    public ActionResponse<int> AddPlayerProfile(int userId, string firstName, string lastName, int fideRating, int KIURating,
        string title)
    {
        var parameters = new DynamicParameters(new {p_UserId = userId, p_FirstName = firstName, p_LastName = lastName, p_FideRating = fideRating, p_KIURating = KIURating, p_Title = title});

        var cmd = new CommandDefinition(
            "player_addPlayerProfile",
            parameters,
            commandType: CommandType.StoredProcedure,
            commandTimeout: 30
        );
        try
        {
            _connection.Execute(cmd);
            var result = new ActionResponse<int>()
            {
                Success = true,
                Message = "Profile added successfully"
            };

            return result;
        }
        catch
        {
            var result = new ActionResponse<int>()
            {
                Success = false,
                Message = "Failed to add profile"
            };
            return result;
        }
    }

    public ActionResponse<int> EditPlayerProfile(int profileId, string title, string fideRating)
    {
        throw new NotImplementedException();
    }

    public ActionResponse<int> DeletePlayerProfile(int profileId)
    {
        throw new NotImplementedException();
    }

    public ActionResponse<PlayerProfile> GetPlayerProfile(int userId)
    {
        throw new NotImplementedException();
    }

    public ActionResponse<IEnumerable<PlayerProfile>> GetAllProfiles()
    {
        throw new NotImplementedException();
    }
}