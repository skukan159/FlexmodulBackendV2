using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.Requests.Customer;
using FlexmodulBackendV2.Contracts.V1.Requests.FmHouse;
using FlexmodulBackendV2.Contracts.V1.Responses;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexmodulBackendV2.Controllers.V1
{
    [EnableCors]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FmHousesController : Controller
    {
        private readonly IFmHousesService _fmHouseService;

        public FmHousesController(IFmHousesService fmHouseService)
        {
            _fmHouseService = fmHouseService;
        }

        [HttpGet(ApiRoutes.FmHouses.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var fmHouses = await _fmHouseService.GetFmHousesAsync();
            var fmHousesResponse = fmHouses.Select(FmHouseToHouseResponse).ToList();
            return Ok(fmHousesResponse);
        }

        [HttpGet(ApiRoutes.FmHouses.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid fmHouseId)
        {
            var fmHouse = await _fmHouseService.GetFmHouseByIdAsync(fmHouseId);
            if (fmHouse == null)
                return NotFound();
            return base.Ok(FmHouseToHouseResponse(fmHouse));
        }

        [HttpPut(ApiRoutes.FmHouses.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid fmHouseId, [FromBody]UpdateFmHouseRequest request)
        {
            var fmHouse = await _fmHouseService.GetFmHouseByIdAsync(fmHouseId);
            fmHouse.HouseType = request.HouseType;
            fmHouse.SquareMeters = request.SquareMeters;

            var updated = await _fmHouseService.UpdateFmHouseAsync(fmHouse);
            if (updated)
                return Ok(FmHouseToHouseResponse(fmHouse));
            return NotFound();

        }

        [HttpPost(ApiRoutes.FmHouses.Create)]
        public async Task<IActionResult> Create([FromBody] CreateFmHouseRequest fmHouseRequest)
        {
            var fmHouse = new FmHouse
            {
                HouseType = fmHouseRequest.HouseType,
                SquareMeters = fmHouseRequest.SquareMeters
            };

            await _fmHouseService.CreateFmHouseAsync(fmHouse);

            var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationuri = baseurl + "/" + ApiRoutes.FmHouses.Get.Replace("{fmHouseId}", fmHouse.Id.ToString());

            var response = FmHouseToHouseResponse(fmHouse);
            return Created(locationuri, response);
        }

        [HttpDelete(ApiRoutes.FmHouses.Delete)]
        public async Task<ActionResult> Delete([FromRoute]Guid fmHouseId)
        {
            var deleted = await _fmHouseService.DeleteFmHouseAsync(fmHouseId);
            if (deleted)
                return NoContent();

            return NotFound();
        }

        public static FmHouseResponse FmHouseToHouseResponse(FmHouse fmHouse)
        {
            return new FmHouseResponse
            {
                Id = fmHouse.Id,
                HouseType = fmHouse.HouseType,
                SquareMeters = fmHouse.SquareMeters
            };
        }
    }
}



