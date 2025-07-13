using ChessClubKIU.DTOs.Users;
using ChessClubKIU.RequestResponse;
using ChessClubKIU.Services.Templates;
using Microsoft.AspNetCore.Mvc;

namespace ChessClubKIU.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserManagementController : ControllerBase
{
    private readonly IUserManagementService _userManagementService;
    private readonly ILogger<UserManagementController> _logger;

    public UserManagementController(IUserManagementService userManagementService, ILogger<UserManagementController> logger)
    {
        _userManagementService = userManagementService;
        _logger = logger;
    }

    [HttpPost("register-user")]
    public IActionResult RegisterUser([FromBody] RegisterUserRequest request) 
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid request");
            return BadRequest(new ActionResponse<int>() {Success = false, Message = "Invalid request format"});
        }
        _logger.LogInformation($"Registering user: {request.Username} : UserManagementController.RegisterUser");
        var response = _userManagementService.RegisterUser(request);
   
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost("login-user")]
    public IActionResult LoginUser([FromBody] LoginUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ActionResponse<int>() {Success = false, Message = "Invalid request format"});
        }

        var response = _userManagementService.Login(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}