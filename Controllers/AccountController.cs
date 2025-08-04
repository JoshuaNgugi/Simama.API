using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    [HttpGet("me")]
    public IActionResult Me()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return Ok(new { id, email, role });
    }
}
