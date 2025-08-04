using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PrescriptionController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPrescriptionDto>>> GetAll()
    {
        var prescriptions = await _context.Prescriptions
            .Include(p => p.Doctor)
            .Include(p => p.Patient)
            .Include(p => p.Drug)
            .Include(p => p.Pharmacist)
            .ToListAsync();

        return Ok(_mapper.Map<List<GetPrescriptionDto>>(prescriptions));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetPrescriptionDto>> GetById(int id)
    {
        var prescription = await _context.Prescriptions
            .Include(p => p.Doctor)
            .Include(p => p.Patient)
            .Include(p => p.Drug)
            .Include(p => p.Pharmacist)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (prescription == null) return NotFound();

        return Ok(_mapper.Map<GetPrescriptionDto>(prescription));
    }

    [HttpPost]
    public async Task<ActionResult<GetPrescriptionDto>> Create(CreatePrescriptionDto dto)
    {
        var prescription = _mapper.Map<Prescription>(dto);
        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        var result = await _context.Prescriptions
            .Include(p => p.Doctor)
            .Include(p => p.Patient)
            .Include(p => p.Drug)
            .Include(p => p.Pharmacist)
            .FirstOrDefaultAsync(p => p.Id == prescription.Id);

        return CreatedAtAction(nameof(GetById), new { id = prescription.Id }, _mapper.Map<GetPrescriptionDto>(result));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatePrescriptionDto dto)
    {
        var prescription = await _context.Prescriptions.FindAsync(id);
        if (prescription == null) return NotFound();

        _mapper.Map(dto, prescription);
        prescription.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var prescription = await _context.Prescriptions.FindAsync(id);
        if (prescription == null) return NotFound();

        prescription.IsDeleted = true;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}