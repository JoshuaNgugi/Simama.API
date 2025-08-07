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
            .Select(p => new
            {
                p.Id,
                p.Status,
                p.Dosage,
                p.FulfilledAt,
                p.PrescribedOn,
                Patient = new
                {
                    p.Patient.Id,
                    p.Patient.FirstName,
                    p.Patient.LastName,
                    p.Patient.Email
                },

                Doctor = new
                {
                    p.Doctor.Id,
                    p.Doctor.FirstName,
                    p.Doctor.LastName,
                    p.Doctor.Email
                },

                Drug = new
                {
                    p.Drug.Id,
                    p.Drug.Name,
                },

                Pharmacist = p.Pharmacist == null ? null : new
                {
                    p.Pharmacist.Id,
                    p.Pharmacist.FirstName,
                    p.Pharmacist.LastName,
                    p.Pharmacist.Email
                }
            })
        .ToListAsync();

        return Ok(prescriptions);
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

        // Manually update only the non-null properties
        if (dto.PatientId.HasValue)
            prescription.PatientId = dto.PatientId.Value;

        if (dto.DoctorId.HasValue)
            prescription.DoctorId = dto.DoctorId.Value;

        if (dto.DrugId.HasValue)
            prescription.DrugId = dto.DrugId.Value;

        if (dto.PharmacistId.HasValue)
            prescription.PharmacistId = dto.PharmacistId.Value;

        if (!string.IsNullOrWhiteSpace(dto.Dosage))
            prescription.Dosage = dto.Dosage;

        if (dto.PrescribedOn.HasValue)
            prescription.PrescribedOn = dto.PrescribedOn.Value;

        if (dto.FulfilledAt.HasValue)
            prescription.FulfilledAt = dto.FulfilledAt.Value;

        if (dto.Status.HasValue)
            prescription.Status = dto.Status.Value;

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