using ChessClubKIU.DAOs.Users;
using ChessClubKIU.DTOs.Users;
using ChessClubKIU.Services.Templates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChessClubKIU.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ILogger<ProfileController> _logger;
    private readonly IPlayerProfileService _playerProfileService;

    public ProfileController(ILogger<ProfileController> logger, IPlayerProfileService playerProfileService)
    {
        _logger = logger;
        _playerProfileService = playerProfileService;
    }

    [HttpGet]
    public IActionResult GetProfile(int UserId)
    {
        return Ok(new { UserId = UserId });
    }

    [HttpPost("add")]
    public IActionResult AddProfile(AddPlayerProfileRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var response = _playerProfileService.AddPlayerProfile(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}