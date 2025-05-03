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

    public UserManagementController(IUserManagementService userManagementService)
    {
        _userManagementService = userManagementService;
    }

    [HttpPost("register-user")]
    public IActionResult RegisterUser([FromBody] RegisterUserRequest request) 
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ActionResponse() {Success = false, Message = "Invalid request format"});
        }
        var response = _userManagementService.RegisterUser(request);
        
        return response.Success ? Ok(response) : BadRequest(response);
    }
}