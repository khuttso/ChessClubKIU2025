using System;
using System.Collections.Generic;
using ChessClubKIU.DAOs.Users;
using ChessClubKIU.DbManagers.Templates;
using ChessClubKIU.DTOs.Users;
using ChessClubKIU.RequestResponse;
using ChessClubKIU.Services.Templates;
using Microsoft.Extensions.Logging;

namespace ChessClubKIU.Services.Standard;

public class PlayerProfileService : IPlayerProfileService
{
    private readonly IPlayerProfileDbManager _playerProfileDbManager;
    private readonly ILogger<PlayerProfileService> _logger;

    public PlayerProfileService(IPlayerProfileDbManager playerProfileDbManager, ILogger<PlayerProfileService> logger)
    {
        _playerProfileDbManager = playerProfileDbManager;
        _logger = logger;
    } 
    
    
    public ActionResponse<IEnumerable<PlayerProfile>> GetPlayerProfiles()
    {
        return _playerProfileDbManager.GetAllProfiles();
    }

    public ActionResponse<PlayerProfile> GetPlayerProfileById(int id)
    {
        throw new NotImplementedException();
    }

    public ActionResponse<int> AddPlayerProfile(AddPlayerProfileRequest playerProfile)
    {
        return _playerProfileDbManager.AddPlayerProfile(
            0,
            playerProfile.FirstName,
            playerProfile.LastName,
            playerProfile.FideRating.Value,
            playerProfile.KIURating.Value,
            playerProfile.Title
        );
    }

    public ActionResponse<int> EditPlayerProfile(EditPlayerProfileRequest playerProfile)
    {
        throw new NotImplementedException();
    }

    public ActionResponse<int> DeletePlayerProfile(int id)
    {
        throw new NotImplementedException();
    }
}