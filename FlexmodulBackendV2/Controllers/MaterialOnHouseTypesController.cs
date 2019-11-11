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
    public class MaterialOnHouseTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MaterialOnHouseTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MaterialOnHouseTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialOnHouseType>>> GetMaterialOnHouseType()
        {
            return await _context.MaterialOnHouseType.ToListAsync();
        }

        // GET: api/MaterialOnHouseTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialOnHouseType>> GetMaterialOnHouseType(int id)
        {
            var materialOnHouseType = await _context.MaterialOnHouseType.FindAsync(id);

            if (materialOnHouseType == null)
            {
                return NotFound();
            }

            return materialOnHouseType;
        }

        // PUT: api/MaterialOnHouseTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterialOnHouseType(int id, MaterialOnHouseType materialOnHouseType)
        {
            if (id != materialOnHouseType.FMHouseTypeId)
            {
                return BadRequest();
            }

            _context.Entry(materialOnHouseType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialOnHouseTypeExists(id))
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

        // POST: api/MaterialOnHouseTypes
        [HttpPost]
        public async Task<ActionResult<MaterialOnHouseType>> PostMaterialOnHouseType(MaterialOnHouseType materialOnHouseType)
        {
            _context.MaterialOnHouseType.Add(materialOnHouseType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MaterialOnHouseTypeExists(materialOnHouseType.FMHouseTypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMaterialOnHouseType", new { id = materialOnHouseType.FMHouseTypeId }, materialOnHouseType);
        }

        // DELETE: api/MaterialOnHouseTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MaterialOnHouseType>> DeleteMaterialOnHouseType(int id)
        {
            var materialOnHouseType = await _context.MaterialOnHouseType.FindAsync(id);
            if (materialOnHouseType == null)
            {
                return NotFound();
            }

            _context.MaterialOnHouseType.Remove(materialOnHouseType);
            await _context.SaveChangesAsync();

            return materialOnHouseType;
        }

        private bool MaterialOnHouseTypeExists(int id)
        {
            return _context.MaterialOnHouseType.Any(e => e.FMHouseTypeId == id);
        }
    }
}
