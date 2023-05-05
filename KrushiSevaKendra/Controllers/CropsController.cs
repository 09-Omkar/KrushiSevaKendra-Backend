using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KrushiSevaKendra.Context;
using KrushiSevaKendra.Models;
using Microsoft.AspNetCore.Authorization;

namespace KrushiSevaKendra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CropsController : ControllerBase
    {
        private readonly KrushiDBContext _context;

        public CropsController(KrushiDBContext context)
        {
            _context = context;
        }

        // GET: api/Crops
        [HttpGet]
   
        public async Task<ActionResult<IEnumerable<Crop>>> GetCrops()
        {
          if (_context.Crops == null)
          {
              return NotFound();
          }
            return await _context.Crops.ToListAsync();
        }

        // GET: api/Crops/5
        [HttpGet("{id}")]
    
        public async Task<ActionResult<Crop>> GetCrop(int id)
        {
          if (_context.Crops == null)
          {
              return NotFound();
          }
            var crop = await _context.Crops.FindAsync(id);

            if (crop == null)
            {
                return NotFound();
            }

            return crop;
        }

        // PUT: api/Crops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        
        public async Task<IActionResult> PutCrop(int id, Crop crop)
        {
            if (id != crop.Id)
            {
                return BadRequest();
            }

            _context.Entry(crop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CropExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Crops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
     
        public async Task<ActionResult<Crop>> PostCrop(Crop crop)
        {
          if (_context.Crops == null)
          {
              return Problem("Entity set 'KrushiDBContext.Crops'  is null.");
          }
            _context.Crops.Add(crop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCrop", new { id = crop.Id }, crop);
        }

        // DELETE: api/Crops/5
        [HttpDelete("{id}")]
       
        public async Task<IActionResult> DeleteCrop(int id)
        {
            if (_context.Crops == null)
            {
                return NotFound();
            }
            var crop = await _context.Crops.FindAsync(id);
            if (crop == null)
            {
                return NotFound();
            }

            _context.Crops.Remove(crop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CropExists(int id)
        {
            return (_context.Crops?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
