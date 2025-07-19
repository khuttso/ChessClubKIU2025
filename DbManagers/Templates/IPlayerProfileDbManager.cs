using ChessClubKIU.DAOs.Users;
using ChessClubKIU.RequestResponse;

namespace ChessClubKIU.DbManagers.Templates;

public interface IPlayerProfileDbManager
{
    ActionResponse<int> AddPlayerProfile(int userId, string firstName, string lastName, int fideRating, int KIURating, string title);
    ActionResponse<int> EditPlayerProfile(int profileId, string title, string fideRating);
    ActionResponse<int> DeletePlayerProfile(int profileId);
    ActionResponse<PlayerProfile> GetPlayerProfile(int userId);
    ActionResponse<IEnumerable<PlayerProfile>> GetAllProfiles();
}