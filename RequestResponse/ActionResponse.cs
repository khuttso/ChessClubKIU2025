namespace ChessClubKIU.RequestResponse;

public class ActionResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? PossibleFix {get; set;}
    public T? Data { get; set; }
}