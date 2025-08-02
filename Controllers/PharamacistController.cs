using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PharmacistController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PharmacistController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPharmacistDto>>> GetAll()
    {
        var pharmacists = await _context.Pharmacists.ToListAsync();
        return Ok(_mapper.Map<List<GetPharmacistDto>>(pharmacists));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetPharmacistDto>> GetById(int id)
    {
        var pharmacist = await _context.Pharmacists.FindAsync(id);
        if (pharmacist == null) return NotFound();

        return Ok(_mapper.Map<GetPharmacistDto>(pharmacist));
    }

    [HttpPost]
    public async Task<ActionResult<GetPharmacistDto>> Create(CreatePharmacistDto dto)
    {
        var pharmacist = _mapper.Map<Pharmacist>(dto);
        _context.Pharmacists.Add(pharmacist);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = pharmacist.Id }, _mapper.Map<GetPharmacistDto>(pharmacist));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatePharmacistDto dto)
    {
        var pharmacist = await _context.Pharmacists.FindAsync(id);
        if (pharmacist == null) return NotFound();

        _mapper.Map(dto, pharmacist);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var pharmacist = await _context.Pharmacists.FindAsync(id);
        if (pharmacist == null) return NotFound();

        pharmacist.IsDeleted = true;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}