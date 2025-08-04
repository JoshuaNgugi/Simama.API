using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public DoctorController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetDoctorDto>>> GetAll()
    {
        var doctors = await _context.Doctors.ToListAsync();
        return Ok(_mapper.Map<List<GetDoctorDto>>(doctors));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetDoctorDto>> GetById(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null) return NotFound();

        return Ok(_mapper.Map<GetDoctorDto>(doctor));
    }

    [HttpPost]
    public async Task<ActionResult<GetDoctorDto>> Create(CreateDoctorDto dto)
    {
        var doctor = _mapper.Map<Doctor>(dto);
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, _mapper.Map<GetDoctorDto>(doctor));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDoctorDto dto)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null) return NotFound();

        _mapper.Map(dto, doctor);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null) return NotFound();

        doctor.IsDeleted = true;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}