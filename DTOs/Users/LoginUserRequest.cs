namespace ChessClubKIU.DTOs.Users;

public class LoginUserRequest
{
    public string UserLogin { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}