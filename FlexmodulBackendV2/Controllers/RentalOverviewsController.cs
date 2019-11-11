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
    public class RentalOverviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RentalOverviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RentalOverviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalOverview>>> GetRentalOverview()
        {
            return await _context.RentalOverview.ToListAsync();
        }

        // GET: api/RentalOverviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalOverview>> GetRentalOverview(int id)
        {
            var rentalOverview = await _context.RentalOverview.FindAsync(id);

            if (rentalOverview == null)
            {
                return NotFound();
            }

            return rentalOverview;
        }

        // PUT: api/RentalOverviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRentalOverview(int id, RentalOverview rentalOverview)
        {
            if (id != rentalOverview.RentalOverviewId)
            {
                return BadRequest();
            }

            _context.Entry(rentalOverview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalOverviewExists(id))
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

        // POST: api/RentalOverviews
        [HttpPost]
        public async Task<ActionResult<RentalOverview>> PostRentalOverview(RentalOverview rentalOverview)
        {
            _context.RentalOverview.Add(rentalOverview);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRentalOverview", new { id = rentalOverview.RentalOverviewId }, rentalOverview);
        }

        // DELETE: api/RentalOverviews/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RentalOverview>> DeleteRentalOverview(int id)
        {
            var rentalOverview = await _context.RentalOverview.FindAsync(id);
            if (rentalOverview == null)
            {
                return NotFound();
            }

            _context.RentalOverview.Remove(rentalOverview);
            await _context.SaveChangesAsync();

            return rentalOverview;
        }

        private bool RentalOverviewExists(int id)
        {
            return _context.RentalOverview.Any(e => e.RentalOverviewId == id);
        }
    }
}
