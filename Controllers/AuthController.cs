using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;

    public AuthController(AppDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        object? user = null;
        string passwordHash = "";
        int userId = 0;

        switch (dto.Role.ToLower())
        {
            case "doctor":
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Email == dto.Email && !d.IsDeleted);
                if (doctor != null)
                {
                    user = doctor;
                    passwordHash = doctor.PasswordHash;
                    userId = doctor.Id;
                }
                break;

            case "pharmacist":
                var pharmacist = await _context.Pharmacists.FirstOrDefaultAsync(ph => ph.Email == dto.Email && !ph.IsDeleted);
                if (pharmacist != null)
                {
                    user = pharmacist;
                    passwordHash = pharmacist.PasswordHash;
                    userId = pharmacist.Id;
                }
                break;

            default:
                return BadRequest("Invalid role provided.");
        }

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, passwordHash))
        {
            return Unauthorized("Invalid credentials.");
        }

        var token = _tokenService.GenerateToken(userId.ToString(), dto.Email, dto.Role);

        return Ok(new { token });
    }
}