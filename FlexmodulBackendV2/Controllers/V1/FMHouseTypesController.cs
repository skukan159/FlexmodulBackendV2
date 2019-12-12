using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.RequestDTO.FmHouseType;
using FlexmodulBackendV2.Contracts.V1.ResponseDTO;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexmodulBackendV2.Controllers.V1
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Employee,Admin,SuperAdmin")]
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
            var fmHouseTypes = await _fmHouseTypeService.GetFmHouseTypesAsync();
            var fmHouseTypesResponse = fmHouseTypes.Select(FmHouseTypeToFmHouseTypeResponse).ToList();
            return Ok(fmHouseTypesResponse);
        }

        [HttpGet(ApiRoutes.FmHouseTypes.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid fmHouseId)
        {
            var fmHouseType = await _fmHouseTypeService.GetFmHouseTypeByIdAsync(fmHouseId);
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
        public async Task<IActionResult> Update([FromRoute]Guid fmHouseTypeId, [FromBody]UpdateFmHouseTypeRequest request)
        {
            var fmHouseType = await _fmHouseTypeService.GetFmHouseTypeByIdAsync(fmHouseTypeId);
            fmHouseType.HouseType = request.HouseType;
            fmHouseType.MaterialsOnHouse = request.MaterialsOnHouse;

            var updated = await _fmHouseTypeService.UpdateFmHouseTypeAsync(fmHouseType);
            if (updated)
                return Ok(FmHouseTypeToFmHouseTypeResponse(fmHouseType));
            return NotFound();

        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost(ApiRoutes.FmHouseTypes.Create)]
        public async Task<IActionResult> Create([FromBody] CreateFmHouseTypeRequest fmHouseRequest)
        {
            var fmHouseType = new FmHouseType
            {
                HouseType = fmHouseRequest.HouseType
            };

            await _fmHouseTypeService.CreateFmHouseTypeAsync(fmHouseType);

            var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationuri = baseurl + "/" + ApiRoutes.FmHouseTypes.Get.Replace("{fmHouseTypeId}", fmHouseType.Id.ToString());

            var response = FmHouseTypeToFmHouseTypeResponse(fmHouseType);
            return Created(locationuri, response);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete(ApiRoutes.FmHouseTypes.Delete)]
        public async Task<ActionResult> Delete([FromRoute]Guid fmHouseId)
        {
            var deleted = await _fmHouseTypeService.DeleteFmHouseTypeAsync(fmHouseId);
            if (deleted)
                return NoContent();

            return NotFound();
        }

        public static FmHouseTypeResponse FmHouseTypeToFmHouseTypeResponse(FmHouseType fmHouseType)
        {
            return new FmHouseTypeResponse
            {
                Id = fmHouseType.Id,
                HouseType = fmHouseType.HouseType,
                MaterialsOnHouse = fmHouseType.MaterialsOnHouse
            };
        }
        
    }
}



