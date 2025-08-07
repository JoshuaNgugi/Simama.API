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
        string firstName = "";
        string lastName = "";

        switch (dto.Role.ToLower())
        {
            case "doctor":
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Email == dto.Email && !d.IsDeleted);
                if (doctor != null)
                {
                    user = doctor;
                    passwordHash = doctor.PasswordHash;
                    userId = doctor.Id;
                    firstName = doctor.FirstName;
                    lastName = doctor.LastName;
                }
                break;

            case "pharmacist":
                var pharmacist = await _context.Pharmacists.FirstOrDefaultAsync(ph => ph.Email == dto.Email && !ph.IsDeleted);
                if (pharmacist != null)
                {
                    user = pharmacist;
                    passwordHash = pharmacist.PasswordHash;
                    userId = pharmacist.Id;
                    firstName = pharmacist.FirstName;
                    lastName = pharmacist.LastName;
                }
                break;

            default:
                return BadRequest("Invalid role provided.");
        }

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, passwordHash))
        {
            return Unauthorized("Invalid credentials.");
        }

        var token = _tokenService.GenerateToken(userId.ToString(), dto.Email, dto.Role, firstName, lastName);

        return Ok(new { token });
    }
}