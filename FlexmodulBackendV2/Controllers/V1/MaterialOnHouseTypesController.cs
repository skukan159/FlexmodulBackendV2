﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.RequestDTO.MaterialOnHouseType;
using FlexmodulBackendV2.Contracts.V1.ResponseDTO;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlexmodulBackendV2.Controllers.V1
{
    [EnableCors]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Roles.Employee + "," + Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
    public class MaterialOnHouseTypesController : ControllerBase
    {

        private readonly IRepository<MaterialOnHouseType> _repositoryService;

        public MaterialOnHouseTypesController(IRepository<MaterialOnHouseType> repositoryService)
        {
            _repositoryService = repositoryService;
        }


        [HttpGet(ApiRoutes.MaterialOnHouseTypes.GetAll)]
        public async Task<ActionResult<IEnumerable<MaterialOnHouseType>>> GetAll()
        {
            var materialOnHouseType = await _repositoryService.GetAsync();
            var materialOnHouseTypeResponses = materialOnHouseType
                .Select(MaterialOnHouseTypeToMaterialOnHouseTypeResponse).ToList();
            return Ok(materialOnHouseTypeResponses);
        }

        [HttpGet(ApiRoutes.MaterialOnHouseTypes.Get)]
        public async Task<ActionResult<MaterialOnHouseType>> Get([FromRoute] Guid id)
        {
            var materialOnHouseType = await _repositoryService
                .GetByIdAsync(id);
            if (materialOnHouseType == null)
                return NotFound();
            return base.Ok(MaterialOnHouseTypeToMaterialOnHouseTypeResponse(materialOnHouseType));
        }

        [Authorize(Roles = Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
        [HttpPut(ApiRoutes.MaterialOnHouseTypes.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] MaterialOnHouseTypeRequest request)
        {
            var materialOnHouseType = await _repositoryService.GetByIdAsync(id);
            materialOnHouseType.FmHouseTypeId = request.FmHouseTypeId;
            materialOnHouseType.MaterialId = request.MaterialId;
            materialOnHouseType.MaterialAmount = request.MaterialAmount;

            var updated = await _repositoryService.UpdateAsync(materialOnHouseType);
            if (updated)
                return Ok(MaterialOnHouseTypeToMaterialOnHouseTypeResponse(materialOnHouseType));
            return NotFound();
        }

        [Authorize(Roles = Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
        [HttpPost(ApiRoutes.MaterialOnHouseTypes.Create)]
        public async Task<IActionResult> Create([FromBody] MaterialOnHouseTypeRequest materialOnHouseTypeRequest)
        {
            var materialOnHouseType = new MaterialOnHouseType
            {
                MaterialId =  materialOnHouseTypeRequest.MaterialId,
                FmHouseTypeId = materialOnHouseTypeRequest.FmHouseTypeId,
                MaterialAmount = materialOnHouseTypeRequest.MaterialAmount
            };

            await _repositoryService.CreateAsync(materialOnHouseType);

            var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationuri = baseurl + "/" + ApiRoutes.MaterialOnHouseTypes.Get.Replace("{materialOnHouseTypeId}", materialOnHouseType.Id.ToString());

            var response = MaterialOnHouseTypeToMaterialOnHouseTypeResponse(materialOnHouseType);
            return Created(locationuri, response);
        }

        [Authorize(Roles = Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
        [HttpDelete(ApiRoutes.MaterialOnHouseTypes.Delete)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _repositoryService
                .DeleteAsync(await _repositoryService.GetByIdAsync(id));
            if (deleted)
                return NoContent();

            return NotFound();
        }

        public static MaterialOnHouseTypeResponse MaterialOnHouseTypeToMaterialOnHouseTypeResponse
            (MaterialOnHouseType materialOnHouseType)
        {
            return new MaterialOnHouseTypeResponse
            {
                Id =  materialOnHouseType.Id,
                MaterialId = materialOnHouseType.MaterialId,
                FmHouseTypeId = materialOnHouseType.FmHouseTypeId,
                MaterialAmount = materialOnHouseType.MaterialAmount
            };
        }
    }
}
