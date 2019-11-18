using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexmodulBackendV2.Controllers.V1
{
    [EnableCors]
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
            return await _context.MaterialOnHouseTypes.ToListAsync();
        }

        // GET: api/MaterialOnHouseTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialOnHouseType>> GetMaterialOnHouseType(int id)
        {
            var materialOnHouseType = await _context.MaterialOnHouseTypes.FindAsync(id);

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
            if (id.ToString() != materialOnHouseType.FmHouseTypeId.ToString())
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
                if (!MaterialOnHouseTypeExists(id.ToString()))
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
            _context.MaterialOnHouseTypes.Add(materialOnHouseType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MaterialOnHouseTypeExists(materialOnHouseType.FmHouseTypeId.ToString()))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMaterialOnHouseType", new { id = materialOnHouseType.FmHouseTypeId }, materialOnHouseType);
        }

        // DELETE: api/MaterialOnHouseTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MaterialOnHouseType>> DeleteMaterialOnHouseType(int id)
        {
            var materialOnHouseType = await _context.MaterialOnHouseTypes.FindAsync(id);
            if (materialOnHouseType == null)
            {
                return NotFound();
            }

            _context.MaterialOnHouseTypes.Remove(materialOnHouseType);
            await _context.SaveChangesAsync();

            return materialOnHouseType;
        }

        private bool MaterialOnHouseTypeExists(string id)
        {
            return _context.MaterialOnHouseTypes.Any(e => e.FmHouseTypeId.ToString() == id);
        }
    }
}
