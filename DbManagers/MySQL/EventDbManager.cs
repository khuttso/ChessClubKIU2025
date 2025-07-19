using System.Data;
using ChessClubKIU.DAOs.Events;
using ChessClubKIU.DbManagers.Templates;
using ChessClubKIU.DTOs.Events;
using ChessClubKIU.RequestResponse;
using Dapper;
using MySqlConnector;

namespace ChessClubKIU.DbManagers.MySQL;

public class EventDbManager : IEventDbManager
{
    private readonly MySqlConnection _connection;

    public EventDbManager(MySqlConnection connection)
    {
        _connection = connection;
    }

    public ActionResponse<int> AddEvent(
        string eventName,
        string eventDescription,
        DateTime startDate,
        DateTime endDate,
        string location,
        int createdByUserId)
    {
        try
        {
            var parameters = new DynamicParameters(new
            {
                p_EventName = eventName,
                p_EventDescription = eventDescription,
                p_StartDate = startDate,
                p_EndDate = endDate,
                p_Location = location,
                p_CreatedByUserId = createdByUserId
            });

            parameters.Add("@ErrorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@ErrorMessage", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

            var cmd = new CommandDefinition(
                "events_AddEvent",
                parameters,
                commandType: CommandType.StoredProcedure,
                commandTimeout: 30
            );

            _connection.Execute(cmd);

            var errorCode = parameters.Get<int>("@ErrorCode");
            var errorMessage = parameters.Get<string>("@ErrorMessage");

            return new ActionResponse<int>
            {
                Success = errorCode == 0,
                Message = errorMessage,
                PossibleFix = errorCode switch
                {
                    400 => "Make sure all fields are filled",
                    409 => "Event already exists or overlaps",
                    _ => "Contact admin"
                }
            };
        }
        catch (Exception e)
        {
            return new ActionResponse<int>
            {
                Success = false,
                Message = e.Message,
                PossibleFix = "Check DB connection or parameters"
            };
        }
    }

    public ActionResponse<int> EditEvent(
        int eventId,
        string eventName,
        string eventDescription,
        DateTime startDate,
        DateTime endDate,
        string location)
    {
        try
        {
            var parameters = new DynamicParameters(new
            {
                p_EventId = eventId,
                p_EventName = eventName,
                p_EventDescription = eventDescription,
                p_StartDate = startDate,
                p_EndDate = endDate,
                p_Location = location
            });

            parameters.Add("@ErrorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@ErrorMessage", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

            var cmd = new CommandDefinition(
                "events_EditEvent",
                parameters,
                commandType: CommandType.StoredProcedure,
                commandTimeout: 30
            );

            _connection.Execute(cmd);

            var errorCode = parameters.Get<int>("@ErrorCode");
            var errorMessage = parameters.Get<string>("@ErrorMessage");

            return new ActionResponse<int>
            {
                Success = errorCode == 0,
                Message = errorMessage,
                Data = eventId,
                PossibleFix = errorCode switch
                {
                    400 => "Invalid data format or missing field",
                    404 => "Event not found",
                    _ => "Contact admin"
                }
            };
        }
        catch (Exception e)
        {
            return new ActionResponse<int>
            {
                Success = false,
                Message = e.Message,
                PossibleFix = "Check DB connection or parameters"
            };
        }
    }

    public ActionResponse<int> DeleteEvent(int eventId)
    {
        try
        {
            var parameters = new DynamicParameters(new
            {
                p_EventId = eventId
            });

            parameters.Add("@ErrorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@ErrorMessage", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

            var cmd = new CommandDefinition(
                "events_DeleteEvent",
                parameters,
                commandType: CommandType.StoredProcedure,
                commandTimeout: 30
            );

            _connection.Execute(cmd);

            var errorCode = parameters.Get<int>("@ErrorCode");
            var errorMessage = parameters.Get<string>("@ErrorMessage");

            return new ActionResponse<int>
            {
                Success = errorCode == 0,
                Message = errorMessage,
                Data = eventId,
                PossibleFix = errorCode switch
                {
                    400 => "Invalid event ID",
                    404 => "Event not found",
                    _ => "Contact admin"
                }
            };
        }
        catch (Exception e)
        {
            return new ActionResponse<int>
            {
                Success = false,
                Message = e.Message,
                PossibleFix = "Check DB connection or parameters"
            };
        }
    }

    public ActionResponse<Event> GetEventByName(string eventName)
    {
        try
        {
            var parameters = new DynamicParameters(new
            {
                p_EventName = eventName
            });

            parameters.Add("@ErrorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@ErrorMessage", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

            var cmd = new CommandDefinition(
                "events_GetEventByName",
                parameters,
                commandType: CommandType.StoredProcedure,
                commandTimeout: 30
            );

            var @event = _connection.QueryFirstOrDefault<Event>(cmd);

            return new ActionResponse<Event>
            {
                Success = @event != null,
                Message = @event != null ? "Event found" : "Event not found",
                Data = @event,
                PossibleFix = @event != null ? null : "Check if the event name is correct"
            };
        }
        catch (Exception e)
        {
            return new ActionResponse<Event>
            {
                Success = false,
                Message = e.Message,
                PossibleFix = "Check DB connection or parameters"
            };
        }
    }

    public ActionResponse<List<Event>> GetAllEvents()
    {
        try
        {
            var cmd = new CommandDefinition(
                "events_GetAllEvents",
                commandType: CommandType.StoredProcedure,
                commandTimeout: 30
            );

            var events = _connection.Query<Event>(cmd).ToList();

            return new ActionResponse<List<Event>>()
            {
                Success = true,
                Message = events.Any() ? "Events retrieved successfully" : "No events found",
                Data = events,
                PossibleFix = null
            };
        }
        catch (Exception e)
        {
            return new ActionResponse<List<Event>>()
            {
                Success = false,
                Message = e.Message,
                PossibleFix = "Check database connection or procedure name",
                Data = null
            };
        }
    }
}
