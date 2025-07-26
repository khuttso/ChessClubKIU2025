using System;
using System.Collections.Generic;
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
        var parameters = new DynamicParameters(new {p_ProfileId = profileId, p_Title = title, p_FideRating = fideRating});

        var cmd = new CommandDefinition(
            "player_editPlayerProfile",
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
                Message = "Profile edited successfully"
            };
            return result;
        }
        catch (Exception e)
        {
            var result = new ActionResponse<int>()
            {
                Success = false,
                Message = "Failed to edit profile"
            };
            return result;
        }
    }

    public ActionResponse<int> DeletePlayerProfile(int profileId)
    {
        var parameters = new DynamicParameters(new {p_ProfileId = profileId});

        var cmd = new CommandDefinition(
            "player_deletePlayerProfile",
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
                Message = "Profile deleted successfully"
            };
            return result;
        }
        catch (Exception e)
        {
            var result = new ActionResponse<int>()
            {
                Success = false,
                Message = "Failed to delete profile"
            };
            return result;
        }
    }

    public ActionResponse<PlayerProfile> GetPlayerProfile(int userId)
    {
        var parameters = new DynamicParameters(new {p_UserId = userId});

        var cmd = new CommandDefinition(
            "player_getPlayerProfile",
            parameters,
            commandType: CommandType.StoredProcedure,
            commandTimeout: 30
        );

        try
        {
            var profile = _connection.QueryFirstOrDefault<PlayerProfile>(cmd);

            var result = new ActionResponse<PlayerProfile>()
            {
                Success = true,
                Message = "Profile retrieved successfully",
                Data = profile
            };

            return result;
        }
        catch (Exception e)
        {
            var result = new ActionResponse<PlayerProfile>()
            {
                Success = false,
                Message = "Failed to retrieve profile",
                Data = null
            };

            return result;
        }
    }

    public ActionResponse<IEnumerable<PlayerProfile>> GetAllProfiles()
    {
        var cmd = new CommandDefinition(
            "player_getAllPlayerProfiles",
            commandType: CommandType.StoredProcedure,
            commandTimeout: 30
        );

        try
        {
            var profiles = _connection.Query<PlayerProfile>(cmd);

            var result = new ActionResponse<IEnumerable<PlayerProfile>>()
            {
                Success = true,
                Message = "Profiles retrieved successfully",
                Data = profiles
            };
            return result;
        }
        catch (Exception e)
        {
            var result = new ActionResponse<IEnumerable<PlayerProfile>>()
            {
                Success = false,
                Message = "Failed to retrieve profiles",
                Data = null
            };
            return result;
        }
    }
}