using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PatientController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPatientDto>>> GetAll()
    {
        var patients = await _context.Patients.ToListAsync();
        return Ok(_mapper.Map<List<GetPatientDto>>(patients));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetPatientDto>> GetById(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return NotFound();

        return Ok(_mapper.Map<GetPatientDto>(patient));
    }

    [HttpPost]
    public async Task<ActionResult<GetPatientDto>> Create(CreatePatientDto dto)
    {
        var patient = _mapper.Map<Patient>(dto);
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = patient.Id }, _mapper.Map<GetPatientDto>(patient));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatePatientDto dto)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return NotFound();

        _mapper.Map(dto, patient);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return NotFound();

        patient.IsDeleted = true;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}