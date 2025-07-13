using ChessClubKIU.DAOs.Users;
using ChessClubKIU.DTOs.Users;
using ChessClubKIU.RequestResponse;
namespace ChessClubKIU.Services.Templates;

public interface IUserManagementService
{
    ActionResponse<int> RegisterUser(RegisterUserRequest request);
    ActionResponse<object> Login(LoginUserRequest request);
}