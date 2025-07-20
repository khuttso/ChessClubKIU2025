using ChessClubKIU.DAOs.Events;
using ChessClubKIU.DTOs.Events;
using ChessClubKIU.RequestResponse;

namespace ChessClubKIU.DbManagers.Templates;

public interface IEventDbManager
{
    ActionResponse<int> AddEvent(string eventName, string eventDescription, DateTime startDate, DateTime endDate, string location,  int createdByUserId);
    ActionResponse<int> EditEvent(int eventId, string eventName, string eventDescription, DateTime startDate, DateTime endDate, string location);
    ActionResponse<int> DeleteEvent(int eventId);
    ActionResponse<Event> GetEventByName(string eventName);
    ActionResponse<List<Event>> GetAllEvents();
}