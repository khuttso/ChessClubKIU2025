using ChessClubKIU.DAOs.Events;
using ChessClubKIU.DTOs.Events;
using ChessClubKIU.RequestResponse;

namespace ChessClubKIU.Services.Templates;

public interface IEventService
{
    ActionResponse<int> AddEvent(CreateEventRequest request, int createdByUserId);
    ActionResponse<int> EditEvent(EditEventRequest request);
    ActionResponse<int> DeleteEvent(int eventId);
    ActionResponse<Event> GetEventByName(string eventName);
    ActionResponse<List<Event>> GetAllEvents();
}