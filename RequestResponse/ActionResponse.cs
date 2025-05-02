namespace ChessClubKIU.RequestResponse;

public class ActionResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? PossibleFix {get; set;}
}