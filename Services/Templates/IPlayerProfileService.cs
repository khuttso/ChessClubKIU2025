using ChessClubKIU.DAOs.Users;
using ChessClubKIU.DTOs.Users;
using ChessClubKIU.RequestResponse;

namespace ChessClubKIU.Services.Templates;

public interface IPlayerProfileService
{
    ActionResponse<IEnumerable<PlayerProfile>> GetPlayerProfiles();
    ActionResponse<PlayerProfile> GetPlayerProfileById(int id);
    ActionResponse<int> AddPlayerProfile(AddPlayerProfileRequest playerProfile);
    ActionResponse<int> EditPlayerProfile(EditPlayerProfileRequest playerProfile);
    ActionResponse<int> DeletePlayerProfile(int id);
}