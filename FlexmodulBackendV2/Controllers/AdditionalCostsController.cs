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
    public class AdditionalCostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdditionalCostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AdditionalCosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdditionalCosts>>> GetAdditionalCosts()
        {
            return await _context.AdditionalCosts.ToListAsync();
        }

        // GET: api/AdditionalCosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdditionalCosts>> GetAdditionalCosts(int id)
        {
            var additionalCosts = await _context.AdditionalCosts.FindAsync(id);

            if (additionalCosts == null)
            {
                return NotFound();
            }

            return additionalCosts;
        }

        // PUT: api/AdditionalCosts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdditionalCosts(int id, AdditionalCosts additionalCosts)
        {
            if (id != additionalCosts.AdditionalCostsId)
            {
                return BadRequest();
            }

            _context.Entry(additionalCosts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdditionalCostsExists(id))
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

        // POST: api/AdditionalCosts
        [HttpPost]
        public async Task<ActionResult<AdditionalCosts>> PostAdditionalCosts(AdditionalCosts additionalCosts)
        {
            _context.AdditionalCosts.Add(additionalCosts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdditionalCosts", new { id = additionalCosts.AdditionalCostsId }, additionalCosts);
        }

        // DELETE: api/AdditionalCosts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AdditionalCosts>> DeleteAdditionalCosts(int id)
        {
            var additionalCosts = await _context.AdditionalCosts.FindAsync(id);
            if (additionalCosts == null)
            {
                return NotFound();
            }

            _context.AdditionalCosts.Remove(additionalCosts);
            await _context.SaveChangesAsync();

            return additionalCosts;
        }

        private bool AdditionalCostsExists(int id)
        {
            return _context.AdditionalCosts.Any(e => e.AdditionalCostsId == id);
        }
    }
}
