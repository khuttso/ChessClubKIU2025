using ChessClubKIU.DTOs.Users;
using ChessClubKIU.RequestResponse;
namespace ChessClubKIU.Services.Templates;

public interface IUserManagementService
{
    ActionResponse RegisterUser(RegisterUserRequest request);
}