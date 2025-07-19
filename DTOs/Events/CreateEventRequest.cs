namespace ChessClubKIU.DTOs.Events;

public class CreateEventRequest
{
    public string EventName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = null!;
}