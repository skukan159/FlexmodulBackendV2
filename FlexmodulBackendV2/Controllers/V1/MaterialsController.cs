using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.Requests.Material;
using FlexmodulBackendV2.Contracts.V1.Responses;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexmodulBackendV2.Controllers.V1
{
    [EnableCors]
    [ApiController]
    public class MaterialsController : Controller
    {
        private readonly IMaterialsService _materialsService;

        public MaterialsController(IMaterialsService materialsService)
        {
            _materialsService = materialsService;
        }

        [HttpGet(ApiRoutes.Materials.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var materials = await _materialsService.GetMaterialsAsync();
            var materialResponses = materials.Select(MaterialToMaterialResponse).ToList();
            return Ok(materialResponses);
        }

        [HttpGet(ApiRoutes.Materials.Get)]
        public async Task<IActionResult> GetCustomer([FromRoute] Guid materialId)
        {
            var material = await _materialsService.GetMaterialByIdAsync(materialId);
            if (material == null)
                return NotFound();
            return base.Ok(MaterialToMaterialResponse(material));
        }

        [HttpPut(ApiRoutes.Materials.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid materialId, [FromBody] UpdateMaterialRequest request)
        {
            var material = await _materialsService.GetMaterialByIdAsync(materialId);
            material.Category = request.Category;
            material.HouseSection = request.HouseSection;
            material.Name = request.Name;
            material.PricePerUnit = request.PricePerUnit;
            material.Supplier = request.Supplier;
            material.Units = request.Units;

            var updated = await _materialsService.UpdateMaterialAsync(material);
            if (updated)
                return Ok(MaterialToMaterialResponse(material));
            return NotFound();

        }

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

            await _materialsService.CreateMaterialAsync(material);

            var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationuri = baseurl + "/" + ApiRoutes.Materials.Get.Replace("{materialId}", material.Id.ToString());

            var response = MaterialToMaterialResponse(material);
            return Created(locationuri, response);
        }

        [HttpDelete(ApiRoutes.Materials.Delete)]
        public async Task<ActionResult> Delete([FromRoute] Guid materialId)
        {
            var deleted = await _materialsService.DeleteMaterialAsync(materialId);
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


