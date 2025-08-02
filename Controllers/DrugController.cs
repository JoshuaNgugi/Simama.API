using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class DrugController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public DrugController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetDrugDto>>> GetAll()
    {
        var drugs = await _context.Drugs.ToListAsync();
        return Ok(_mapper.Map<List<GetDrugDto>>(drugs));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetDrugDto>> GetById(int id)
    {
        var drug = await _context.Drugs.FindAsync(id);
        if (drug == null) return NotFound();

        return Ok(_mapper.Map<GetDrugDto>(drug));
    }

    [HttpPost]
    public async Task<ActionResult<GetDrugDto>> Create(CreateDrugDto dto)
    {
        var Drug = _mapper.Map<Drug>(dto);
        _context.Drugs.Add(Drug);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = Drug.Id }, _mapper.Map<GetDrugDto>(Drug));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDrugDto dto)
    {
        var drug = await _context.Drugs.FindAsync(id);
        if (drug == null) return NotFound();

        _mapper.Map(dto, drug);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var drug = await _context.Drugs.FindAsync(id);
        if (drug == null) return NotFound();

        drug.IsDeleted = true;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}