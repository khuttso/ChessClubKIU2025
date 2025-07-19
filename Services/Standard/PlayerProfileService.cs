using ChessClubKIU.DAOs.Users;
using ChessClubKIU.DbManagers.Templates;
using ChessClubKIU.DTOs.Users;
using ChessClubKIU.RequestResponse;
using ChessClubKIU.Services.Templates;

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
        throw new NotImplementedException();
    }

    public ActionResponse<PlayerProfile> GetPlayerProfileById(int id)
    {
        throw new NotImplementedException();
    }

    public ActionResponse<int> AddPlayerProfile(AddPlayerProfileRequest playerProfile)
    {
        try
        {
            var result = _playerProfileDbManager.AddPlayerProfile(
                2,
                playerProfile.FirstName,
                playerProfile.LastName,
                0,
                1100,
                ""
            );
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            var response = new ActionResponse<int>()
            {
                Success = false,
                Message = ex.Message
            };
            return response;
        }
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