﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.Requests.FmHouse;
using FlexmodulBackendV2.Contracts.V1.Requests.FmHouseType;
using FlexmodulBackendV2.Contracts.V1.Responses;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexmodulBackendV2.Controllers.V1
{
    [EnableCors]
    [ApiController]
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


