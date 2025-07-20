namespace ChessClubKIU.DAOs.Events;

public class Event
{
    public int EventId { get; set; }
    public string EventName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = null!;
    public int CreatedByUserId { get; set; }
}