using ChessClubKIU.DbManagers.Templates;
using ChessClubKIU.DTOs.Users;
using ChessClubKIU.RequestResponse;
using ChessClubKIU.Services.Algorithms;
using ChessClubKIU.Services.Templates;


namespace ChessClubKIU.Services.Standard;

public class UserManagementService : IUserManagementService
{
    private readonly IUserManagementDbManager _userManagementDbManager;
    private readonly IPasswordHasher _passwordHasher;
    public UserManagementService(IUserManagementDbManager userManagementDbManager, IPasswordHasher passwordHasher)
    {
        _userManagementDbManager = userManagementDbManager;
        _passwordHasher = passwordHasher;
    }
        
    public ActionResponse RegisterUser(RegisterUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return new ActionResponse()
            {
                Success = false,
                Message = "Password required"
            };
        }

        if (request.Password != request.ConfirmPassword)
        {
            return new ActionResponse()
            {
                Success = false,
                Message = "Passwords do not match",
                PossibleFix = "Type same password"
            };
        }
        
        var (hash, salt) = _passwordHasher.HashPassword(request.Password);

        var result = _userManagementDbManager.AddUser(
            request.Username,
            request.Email,
            request.Gender,
            hash,
            salt
        );

        return result;
    }
}