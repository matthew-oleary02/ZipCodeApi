using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZipCodeApi.Data;
using ZipCodeApi.Models;

namespace ZipCodeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Endpoint will be /api/ZipCodes
    public class ZipCodesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ZipCodesController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/ZipCodes - Returns all records
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.ZipCodes.ToListAsync());

        // GET /api/ZipCodes/{id} - Returns a single record by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var zip = await _context.ZipCodes.FindAsync(id);
            return zip == null ? NotFound() : Ok(zip);
        }

        // POST /api/ZipCodes - Adds a new ZIP record
        [HttpPost]
        public async Task<IActionResult> Create(ZipCode zip)
        {
            _context.ZipCodes.Add(zip);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = zip.Id }, zip);
        }

        // PUT /api/ZipCodes/{id} - Updates an existing record
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ZipCode updated)
        {
            if (id != updated.Id) return BadRequest();
            _context.Entry(updated).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/ZipCodes/{id} - Deletes a record by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var zip = await _context.ZipCodes.FindAsync(id);
            if (zip == null) return NotFound();
            _context.ZipCodes.Remove(zip);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}