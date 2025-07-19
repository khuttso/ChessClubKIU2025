using ChessClubKIU.DAOs.Events;
using ChessClubKIU.DbManagers.Templates;
using ChessClubKIU.DTOs.Events;
using ChessClubKIU.RequestResponse;
using ChessClubKIU.Services.Templates;
using Microsoft.Extensions.Logging;

namespace ChessClubKIU.Services.Standard;

public class EventService : IEventService
{
    private readonly IEventDbManager _eventDbManager;
    private readonly ILogger<EventService> _logger;

    public EventService(IEventDbManager eventDbManager, ILogger<EventService> logger)
    {
        _eventDbManager = eventDbManager;
        _logger = logger;
    }

    public ActionResponse<int> AddEvent(CreateEventRequest request, int createdByUserId)
    {
        _logger.LogInformation("Attempting to add event: {Name} by user {UserId}", request.EventName, createdByUserId);

        if (string.IsNullOrWhiteSpace(request.EventName))
        {
            _logger.LogWarning("Event name is missing");
            return new ActionResponse<int>
            {
                Success = false,
                Message = "Event name is required",
                PossibleFix = "Provide a non-empty event name"
            };
        }

        if (request.StartDate >= request.EndDate)
        {
            _logger.LogWarning("Invalid dates: Start {StartDate} >= End {EndDate}", request.StartDate, request.EndDate);
            return new ActionResponse<int>
            {
                Success = false,
                Message = "Start date must be before end date",
                PossibleFix = "Adjust the event dates"
            };
        }

        return _eventDbManager.AddEvent(
            request.EventName,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.Location,
            createdByUserId
        );
    }

    public ActionResponse<int> EditEvent(EditEventRequest request)
    {
        _logger.LogInformation("Attempting to edit event ID: {Id}", request.EventId);

        if (request.EventId <= 0)
        {
            _logger.LogWarning("Invalid event ID: {Id}", request.EventId);
            return new ActionResponse<int>
            {
                Success = false,
                Message = "Invalid event ID",
                PossibleFix = "Event ID must be greater than zero"
            };
        }

        if (string.IsNullOrWhiteSpace(request.EventName))
        {
            _logger.LogWarning("Missing event name for event ID: {Id}", request.EventId);
            return new ActionResponse<int>
            {
                Success = false,
                Message = "Event name is required",
                PossibleFix = "Provide a non-empty event name"
            };
        }

        if (request.StartDate >= request.EndDate)
        {
            _logger.LogWarning("Invalid date range for event ID {Id}: Start {StartDate} >= End {EndDate}",
                request.EventId, request.StartDate, request.EndDate);
            return new ActionResponse<int>
            {
                Success = false,
                Message = "Start date must be before end date",
                PossibleFix = "Adjust the event dates"
            };
        }

        return _eventDbManager.EditEvent(
            request.EventId,
            request.EventName,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.Location
        );
    }

    public ActionResponse<int> DeleteEvent(int eventId)
    {
        _logger.LogInformation("Attempting to delete event ID: {Id}", eventId);

        if (eventId <= 0)
        {
            _logger.LogWarning("Invalid event ID provided: {Id}", eventId);
            return new ActionResponse<int>
            {
                Success = false,
                Message = "Invalid event ID",
                PossibleFix = "Event ID must be greater than zero"
            };
        }

        return _eventDbManager.DeleteEvent(eventId);
    }

    public ActionResponse<Event> GetEventByName(string eventName)
    {
        _logger.LogInformation("Attempting to fetch event by name: {Name}", eventName);

        if (string.IsNullOrWhiteSpace(eventName))
        {
            _logger.LogWarning("Event name query was empty or null");
            return new ActionResponse<Event>
            {
                Success = false,
                Message = "Event name is required",
                PossibleFix = "Provide a valid event name"
            };
        }

        return _eventDbManager.GetEventByName(eventName);
    }

    public ActionResponse<List<Event>> GetAllEvents()
    {
        _logger.LogInformation("Fetching all events from database");
        return _eventDbManager.GetAllEvents();
    }
}
