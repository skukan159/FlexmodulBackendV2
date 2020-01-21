using System;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.RequestDTO.FmHouseType;
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
    public class FmHouseTypesController : Controller
    {

        private readonly IFmHouseTypesService _fmHouseTypeService;

        public FmHouseTypesController(IFmHouseTypesService fmHouseTypeService)
        {
            _fmHouseTypeService = fmHouseTypeService;
        }

        [HttpGet(ApiRoutes.FmHouseTypes.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var fmHouseTypes = await _fmHouseTypeService.GetAsync();
            var fmHouseTypesResponse = fmHouseTypes.Select(FmHouseTypeToFmHouseTypeResponse).ToList();
            return Ok(fmHouseTypesResponse);
        }

        [HttpGet(ApiRoutes.FmHouseTypes.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid fmHouseTypeId)
        {
            var fmHouseType = await _fmHouseTypeService.GetByIdAsync(fmHouseTypeId);
            if (fmHouseType == null)
                return NotFound();
            return base.Ok(FmHouseTypeToFmHouseTypeResponse(fmHouseType));
        }

        [HttpGet(ApiRoutes.FmHouseTypes.GetByType)]
        public async Task<IActionResult> Get([FromRoute]int houseType)
        {
            var fmHouseType = await _fmHouseTypeService.GetFmHouseTypeByTypeAsync(houseType);
            if (fmHouseType == null)
                return NotFound();
            return base.Ok(FmHouseTypeToFmHouseTypeResponse(fmHouseType));
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut(ApiRoutes.FmHouseTypes.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid fmHouseTypeId, [FromBody]FmHouseTypeRequest request)
        {
            var fmHouseType = await _fmHouseTypeService.GetByIdAsync(fmHouseTypeId);
            fmHouseType.HouseType = request.HouseType;

            var updated = await _fmHouseTypeService.UpdateAsync(fmHouseType);
            if (updated)
                return Ok(FmHouseTypeToFmHouseTypeResponse(fmHouseType));
            return NotFound();

        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost(ApiRoutes.FmHouseTypes.Create)]
        public async Task<IActionResult> Create([FromBody] FmHouseTypeRequest fmHouseRequest)
        {
            var fmHouseType = new FmHouseType
            {
                HouseType = fmHouseRequest.HouseType
            };

            await _fmHouseTypeService.CreateAsync(fmHouseType);

            var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationuri = baseurl + "/" + ApiRoutes.FmHouseTypes.Get.Replace("{fmHouseTypeId}", fmHouseType.Id.ToString());

            var response = FmHouseTypeToFmHouseTypeResponse(fmHouseType);
            return Created(locationuri, response);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete(ApiRoutes.FmHouseTypes.Delete)]
        public async Task<ActionResult> Delete([FromRoute]Guid fmHouseId)
        {
            var fmHouse = await _fmHouseTypeService.GetByIdAsync(fmHouseId);
            var deleted = await _fmHouseTypeService.DeleteAsync(fmHouse);
            if (deleted)
                return NoContent();

            return NotFound();
        }

        public static FmHouseTypeResponse FmHouseTypeToFmHouseTypeResponse(FmHouseType fmHouseType)
        {
            var materialsOnHouseType = fmHouseType
                .MaterialsOnHouse
                .Select(MaterialOnHouseTypesController.MaterialOnHouseTypeToMaterialOnHouseTypeResponse)
                .ToList();

            return new FmHouseTypeResponse
            {
                Id = fmHouseType.Id,
                HouseType = fmHouseType.HouseType,
                MaterialsOnHouse = materialsOnHouseType
            };
        }
        
    }
}



