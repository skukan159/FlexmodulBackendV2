using System;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.RequestDTO.FmHouse;
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
        Roles = Roles.Employee+","+Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
    public class FmHousesController : Controller
    {
        private readonly IFmHouseService _fmHouseService;

        public FmHousesController(IFmHouseService fmHouseService)
        {
            _fmHouseService = fmHouseService;
        }

        [HttpGet(ApiRoutes.FmHouses.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var fmHouses = await _fmHouseService.GetAsync();
            var fmHousesResponse = fmHouses.Select(FmHouseToHouseResponse).ToList();
            return Ok(fmHousesResponse);
        }

        [HttpGet(ApiRoutes.FmHouses.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid fmHouseId)
        {
            var fmHouse = await _fmHouseService.GetByIdAsync(fmHouseId);
            if (fmHouse == null)
                return NotFound();
            return base.Ok(FmHouseToHouseResponse(fmHouse));
        }

        [Authorize(Roles = Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
        [HttpPut(ApiRoutes.FmHouses.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid fmHouseId, [FromBody]FmHouseRequest request)
        {
            var fmHouse = await _fmHouseService.GetByIdAsync(fmHouseId);
            fmHouse.HouseType = request.HouseType;
            fmHouse.SquareMeters = request.SquareMeters;

            var updated = await _fmHouseService.UpdateAsync(fmHouse);
            if (updated)
                return Ok(FmHouseToHouseResponse(fmHouse));
            return NotFound();

        }

        [Authorize(Roles = Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
        [HttpPost(ApiRoutes.FmHouses.Create)]
        public async Task<IActionResult> Create([FromBody] FmHouseRequest fmHouseRequest)
        {
            var fmHouse = new FmHouse
            {
                HouseType = fmHouseRequest.HouseType,
                SquareMeters = fmHouseRequest.SquareMeters
            };

            await _fmHouseService.CreateAsync(fmHouse);

            var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationuri = baseurl + "/" + ApiRoutes.FmHouses.Get.Replace("{fmHouseId}", fmHouse.Id.ToString());

            var response = FmHouseToHouseResponse(fmHouse);
            return Created(locationuri, response);
        }

        [Authorize(Roles = Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
        [HttpDelete(ApiRoutes.FmHouses.Delete)]
        public async Task<ActionResult> Delete([FromRoute]Guid fmHouseId)
        {
            var fmHouse = await _fmHouseService.GetByIdAsync(fmHouseId);
            var deleted = await _fmHouseService.DeleteAsync(fmHouse);
            if (deleted)
                return NoContent();

            return NotFound();
        }

        public static FmHouseResponse FmHouseToHouseResponse(FmHouse fmHouse)
        {
            return new FmHouseResponse
            {
                Id = fmHouse.Id,
                HouseType = fmHouse.HouseType.HouseType,
                SquareMeters = fmHouse.SquareMeters,
                MaterialsOnHouse = fmHouse.HouseType.MaterialsOnHouse
            };
        }
    }
}



