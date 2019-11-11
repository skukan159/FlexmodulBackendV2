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
    public class ProductionInformationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductionInformationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductionInformations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductionInformation>>> GetProductionInformation()
        {
            return await _context.ProductionInformation.ToListAsync();
        }

        // GET: api/ProductionInformations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductionInformation>> GetProductionInformation(int id)
        {
            var productionInformation = await _context.ProductionInformation.FindAsync(id);

            if (productionInformation == null)
            {
                return NotFound();
            }

            return productionInformation;
        }

        // PUT: api/ProductionInformations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductionInformation(int id, ProductionInformation productionInformation)
        {
            if (id != productionInformation.ProductionInformationId)
            {
                return BadRequest();
            }

            _context.Entry(productionInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductionInformationExists(id))
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

        // POST: api/ProductionInformations
        [HttpPost]
        public async Task<ActionResult<ProductionInformation>> PostProductionInformation(ProductionInformation productionInformation)
        {
            _context.ProductionInformation.Add(productionInformation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductionInformation", new { id = productionInformation.ProductionInformationId }, productionInformation);
        }

        // DELETE: api/ProductionInformations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductionInformation>> DeleteProductionInformation(int id)
        {
            var productionInformation = await _context.ProductionInformation.FindAsync(id);
            if (productionInformation == null)
            {
                return NotFound();
            }

            _context.ProductionInformation.Remove(productionInformation);
            await _context.SaveChangesAsync();

            return productionInformation;
        }

        private bool ProductionInformationExists(int id)
        {
            return _context.ProductionInformation.Any(e => e.ProductionInformationId == id);
        }
    }
}
