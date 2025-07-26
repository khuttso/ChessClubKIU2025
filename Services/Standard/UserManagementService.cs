using System;
using ChessClubKIU.DAOs.Users;
using ChessClubKIU.DbManagers.Templates;
using ChessClubKIU.DTOs.Users;
using ChessClubKIU.RequestResponse;
using ChessClubKIU.Services.Algorithms;
using ChessClubKIU.Services.Templates;


namespace ChessClubKIU.Services.Standard;

public class UserManagementService : IUserManagementService
{
    private readonly IUserManagementDbManager _userManagementDbManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasher _passwordHasher;
    public UserManagementService(IUserManagementDbManager userManagementDbManager, IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService)
    {
        _userManagementDbManager = userManagementDbManager;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }
        
    public ActionResponse<int> RegisterUser(RegisterUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return new ActionResponse<int>()
            {
                Success = false,
                Message = "Password required"
            };
        }

        if (request.Password != request.ConfirmPassword)
        {
            return new ActionResponse<int>()
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

    public ActionResponse<object> Login(LoginUserRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.UserLogin) || string.IsNullOrWhiteSpace(request.Password))
            {
                return new ActionResponse<object>()
                {
                    Success = false,
                    Message = "Login/Password required"
                };
            }

            var userResponse = _userManagementDbManager.GetUserByCredential(request.UserLogin);
            
            if (!userResponse.Success || userResponse.Data == null)
            {
                return new ActionResponse<object>()
                {
                    Success = false,
                    Message = "Invalid Credentials"
                };
            }

            var user = userResponse.Data;

            if (!_passwordHasher.VerifyHashedPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ActionResponse<object>()
                {
                    Success = false,
                    Message = "Invalid Credentials"
                };
            }
            var token = _jwtTokenService.GenerateJwtToken(user.UserId, user.Username);
            var refreshToken = _passwordHasher.GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            
            var updateResult = _userManagementDbManager.UpdateRefreshToken(user.UserId, refreshToken, refreshTokenExpiry);

            if (!updateResult.Success)
            {
                return new ActionResponse<object>()
                {
                    Success = false,
                    Message = "Update refresh token failed" + updateResult.Message
                };
            }

            return new ActionResponse<object>()
            {
                Success = true,
                Message = "Successful login",
                Data = new { Token = token, UserId = user.UserId, RefreshToken = refreshToken, Expiry = refreshTokenExpiry }
            };
        }
        catch (Exception e)
        {
            return new ActionResponse<object>()
            {
                Success = false,
                Message = $"Login failed: {e.Message}",
                PossibleFix = "Try again later, Username or password does not match"
            };
        }
    }
}