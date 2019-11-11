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
    public class FMHouseTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FMHouseTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FMHouseTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FMHouseType>>> GetFMHouseType()
        {
            return await _context.FMHouseType.ToListAsync();
        }

        // GET: api/FMHouseTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FMHouseType>> GetFMHouseType(int id)
        {
            var fMHouseType = await _context.FMHouseType.FindAsync(id);

            if (fMHouseType == null)
            {
                return NotFound();
            }

            return fMHouseType;
        }

        // PUT: api/FMHouseTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFMHouseType(int id, FMHouseType fMHouseType)
        {
            if (id != fMHouseType.FMHouseTypeId)
            {
                return BadRequest();
            }

            _context.Entry(fMHouseType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FMHouseTypeExists(id))
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

        // POST: api/FMHouseTypes
        [HttpPost]
        public async Task<ActionResult<FMHouseType>> PostFMHouseType(FMHouseType fMHouseType)
        {
            _context.FMHouseType.Add(fMHouseType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFMHouseType", new { id = fMHouseType.FMHouseTypeId }, fMHouseType);
        }

        // DELETE: api/FMHouseTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FMHouseType>> DeleteFMHouseType(int id)
        {
            var fMHouseType = await _context.FMHouseType.FindAsync(id);
            if (fMHouseType == null)
            {
                return NotFound();
            }

            _context.FMHouseType.Remove(fMHouseType);
            await _context.SaveChangesAsync();

            return fMHouseType;
        }

        private bool FMHouseTypeExists(int id)
        {
            return _context.FMHouseType.Any(e => e.FMHouseTypeId == id);
        }
    }
}
