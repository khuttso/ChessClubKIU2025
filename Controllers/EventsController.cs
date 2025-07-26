using ChessClubKIU.DTOs.Events;
using ChessClubKIU.RequestResponse;
using ChessClubKIU.Services.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChessClubKIU.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly ILogger<EventsController> _logger;

    public EventsController(IEventService eventService, ILogger<EventsController> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    [HttpPost("add")]
    public IActionResult AddEvent([FromBody] CreateEventRequest request)
    {
        _logger.LogInformation("Received request to add event: {Name}", request.EventName);

        int createdByUserId = 1; // TODO: Replace with real user ID from auth
        var response = _eventService.AddEvent(request, createdByUserId);

        if (!response.Success)
        {
            _logger.LogWarning("Failed to add event: {Reason}", response.Message);
            return BadRequest(response);
        }

        _logger.LogInformation("Event added successfully: {Name}", request.EventName);
        return Ok(response);
    }

    [HttpPut("edit")]
    public IActionResult EditEvent([FromBody] EditEventRequest request)
    {
        _logger.LogInformation("Received request to edit event ID: {Id}", request.EventId);

        var response = _eventService.EditEvent(request);

        if (!response.Success)
        {
            _logger.LogWarning("Failed to edit event ID {Id}: {Reason}", request.EventId, response.Message);
            return BadRequest(response);
        }

        _logger.LogInformation("Event updated successfully: {Id}", request.EventId);
        return Ok(response);
    }

    [HttpDelete("{eventId}")]
    public IActionResult DeleteEvent(int eventId)
    {
        _logger.LogInformation("Received request to delete event ID: {Id}", eventId);

        var response = _eventService.DeleteEvent(eventId);

        if (!response.Success)
        {
            _logger.LogWarning("Failed to delete event ID {Id}: {Reason}", eventId, response.Message);
            return BadRequest(response);
        }

        _logger.LogInformation("Event deleted successfully: {Id}", eventId);
        return Ok(response);
    }

    [HttpGet("by-name")]
    public IActionResult GetEventByName([FromQuery] string name)
    {
        _logger.LogInformation("Received request to get event by name: {Name}", name);

        var response = _eventService.GetEventByName(name);

        if (!response.Success || response.Data == null)
        {
            _logger.LogWarning("Event not found: {Name}", name);
            return NotFound(response);
        }

        _logger.LogInformation("Event found: {Name}", name);
        return Ok(response);
    }

    [HttpGet("all")]
    public IActionResult GetAllEvents()
    {
        _logger.LogInformation("Received request to get all events");

        var response = _eventService.GetAllEvents();

        _logger.LogInformation("Returned {Count} events", response.Data?.Count ?? 0);
        return Ok(response);
    }
}
