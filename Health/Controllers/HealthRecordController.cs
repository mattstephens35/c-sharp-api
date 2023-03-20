using Health.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Health.Controllers;

[ApiController]
[Route("api")]
public class HealthRecordController : ControllerBase
{
    private readonly HealthCareContext _context;

    public HealthRecordController(HealthCareContext context)
    {
        _context = context;
    }

    [HttpGet("healthrecords")]
    public async Task<ActionResult<IEnumerable<HealthRecord>>> GetHealthRecords()
    {
        return await _context.HealthRecords.ToListAsync();
    }

    [HttpPost("healthrecords")]
    public async Task<ActionResult<HealthRecord>> PostHealthRecord(HealthRecord healthRecord)
    {
        _context.HealthRecords.Add(healthRecord);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHealthRecord), new { id = healthRecord.Id }, healthRecord);
    }

    [HttpGet("healthrecords/{id}")]
    public async Task<ActionResult<HealthRecord>> GetHealthRecord(int id)
    {
        var healthRecord = await _context.HealthRecords.FindAsync(id);

        if (healthRecord == null)
        {
            return NotFound();
        }

        return healthRecord;
    }
}
