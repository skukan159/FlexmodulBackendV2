using System;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.RequestDTO.Material;
using FlexmodulBackendV2.Contracts.V1.ResponseDTO;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlexmodulBackendV2.Controllers.V1
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Roles.Employee + "," + Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
    public class MaterialsController : Controller
    {
        private readonly IRepository<Material> _materialsService;

        public MaterialsController(IRepository<Material> materialsService)
        {
            _materialsService = materialsService;
        }

        [HttpGet(ApiRoutes.Materials.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var materials = await _materialsService.GetAsync();
            var materialResponses = materials.Select(MaterialToMaterialResponse).ToList();
            return Ok(materialResponses);
        }

        [HttpGet(ApiRoutes.Materials.Get)]
        public async Task<IActionResult> GetCustomer([FromRoute] Guid materialId)
        {
            var material = await _materialsService.GetByIdAsync(materialId);
            if (material == null)
                return NotFound();
            return base.Ok(MaterialToMaterialResponse(material));
        }

        [Authorize(Roles = Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
        [HttpPut(ApiRoutes.Materials.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid materialId, [FromBody] UpdateMaterialRequest request)
        {
            var material = await _materialsService.GetByIdAsync(materialId);
            material.Category = request.Category;
            material.HouseSection = request.HouseSection;
            material.Name = request.Name;
            material.PricePerUnit = request.PricePerUnit;
            material.Supplier = request.Supplier;
            material.Units = request.Units;

            var updated = await _materialsService.UpdateAsync(material);
            if (updated)
                return Ok(MaterialToMaterialResponse(material));
            return NotFound();

        }
        [Authorize(Roles = Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
        [HttpPost(ApiRoutes.Materials.Create)]
        public async Task<IActionResult> Create([FromBody] CreateMaterialRequest materialRequest)
        {
            var material = new Material
            {
                Name = materialRequest.Name,
                Category = materialRequest.Category,
                HouseSection = materialRequest.HouseSection,
                PricePerUnit = materialRequest.PricePerUnit,
                Supplier = materialRequest.Supplier,
                Units = materialRequest.Units
            };

            await _materialsService.CreateAsync(material);

            var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationuri = baseurl + "/" + ApiRoutes.Materials.Get.Replace("{materialId}", material.Id.ToString());

            var response = MaterialToMaterialResponse(material);
            return Created(locationuri, response);
        }

        [Authorize(Roles = Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
        [HttpDelete(ApiRoutes.Materials.Delete)]
        public async Task<ActionResult> Delete([FromRoute] Guid materialId)
        {
            var deleted = await _materialsService
                .DeleteAsync(await _materialsService.GetByIdAsync(materialId));
            if (deleted)
                return NoContent();

            return NotFound();
        }

        public static MaterialResponse MaterialToMaterialResponse(Material material)
        {
            return new MaterialResponse
            {
                Id = material.Id,
                Category = material.Category,
                HouseSection = material.HouseSection,
                Name = material.Name,
                PricePerUnit = material.PricePerUnit,
                Supplier = material.Supplier,
                Units = material.Units
            };
        }
    }
}


