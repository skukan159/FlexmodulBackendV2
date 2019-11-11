using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlexmodulAPI.Models;
using FlexmodulBackendV2.Data;

namespace FlexmodulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FMHousesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FMHousesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FMHouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FMHouse>>> GetFMHouse()
        {
            return await _context.FMHouse.ToListAsync();
        }

        // GET: api/FMHouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FMHouse>> GetFMHouse(int id)
        {
            var fMHouse = await _context.FMHouse.FindAsync(id);

            if (fMHouse == null)
            {
                return NotFound();
            }

            return fMHouse;
        }

        // PUT: api/FMHouses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFMHouse(int id, FMHouse fMHouse)
        {
            if (id != fMHouse.FMHouseId)
            {
                return BadRequest();
            }

            _context.Entry(fMHouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FMHouseExists(id))
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

        // POST: api/FMHouses
        [HttpPost]
        public async Task<ActionResult<FMHouse>> PostFMHouse(FMHouse fMHouse)
        {
            _context.FMHouse.Add(fMHouse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFMHouse", new { id = fMHouse.FMHouseId }, fMHouse);
        }

        // DELETE: api/FMHouses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FMHouse>> DeleteFMHouse(int id)
        {
            var fMHouse = await _context.FMHouse.FindAsync(id);
            if (fMHouse == null)
            {
                return NotFound();
            }

            _context.FMHouse.Remove(fMHouse);
            await _context.SaveChangesAsync();

            return fMHouse;
        }

        private bool FMHouseExists(int id)
        {
            return _context.FMHouse.Any(e => e.FMHouseId == id);
        }
    }
}
