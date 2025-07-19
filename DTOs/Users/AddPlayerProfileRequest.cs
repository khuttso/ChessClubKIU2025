namespace ChessClubKIU.DTOs.Users;

public class AddPlayerProfileRequest
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    
    public int? FideRating { get; set; }
    public int? KIURating { get; set; }
    public string? Title { get; set; }        // e.g., GM, IM, FM
}