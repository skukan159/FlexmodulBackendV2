using System;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.RequestDTO.ProductionInformation;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Employee,Admin,SuperAdmin")]
    public class ProductionInformationsController : Controller
    {
        private readonly IRepository<ProductionInformation> _productionInformationsService;

        public ProductionInformationsController(IRepository<ProductionInformation> productionInformationsService)
        {
            _productionInformationsService = productionInformationsService;
        }

        [HttpGet(ApiRoutes.ProductionInformations.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var productionInformations = await _productionInformationsService.GetAsync();
            var productionInformationResponse = productionInformations.Select(ProdInfoToProdInfoResponse).ToList();
            return Ok(productionInformationResponse);
        }

        [HttpGet(ApiRoutes.ProductionInformations.Get)]
        public async Task<IActionResult> GetById([FromRoute] Guid productionInformationId)
        {
            var productionInformation = await _productionInformationsService.GetByIdAsync(productionInformationId);
            if (productionInformation == null)
                return NotFound();
            return base.Ok(ProdInfoToProdInfoResponse(productionInformation));
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut(ApiRoutes.ProductionInformations.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid productionInformationId, [FromBody] UpdateProductionInformationRequest request)
        {
            var productionInformation = await _productionInformationsService.GetByIdAsync(productionInformationId);
            productionInformation.House = request.House;
            productionInformation.Rents = request.Rents;
            productionInformation.Customer = request.Customer;
            productionInformation.ExteriorWalls = request.ExteriorWalls;
            productionInformation.Ventilation = request.Ventilation;
            productionInformation.Note = request.Note;
            productionInformation.ProductionPrice = request.ProductionPrice;
            productionInformation.AdditionalCosts = request.AdditionalCosts;
            productionInformation.LastUpdatedBy = request.LastUpdatedBy;
            productionInformation.LastUpdatedDate = request.LastUpdatedDate;
            productionInformation.IsActive = request.IsActive;

            var updated = await _productionInformationsService.UpdateAsync(productionInformation);
            if (updated)
                return Ok(ProdInfoToProdInfoResponse(productionInformation));
            return NotFound();

        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost(ApiRoutes.ProductionInformations.Create)]
        public async Task<IActionResult> Create([FromBody] CreateProductionInformationRequest productionInformationRequest)
        {
            var productionInformation = new ProductionInformation
            {
                House = productionInformationRequest.House,
                Customer = productionInformationRequest.Customer,
                ExteriorWalls = productionInformationRequest.ExteriorWalls,
                Ventilation = productionInformationRequest.Ventilation,
                Note = productionInformationRequest.Note,
                ProductionPrice = productionInformationRequest.ProductionPrice,
                ProductionDate = productionInformationRequest.ProductionDate,
                LastUpdatedDate = productionInformationRequest.LastUpdatedDate,
                LastUpdatedBy = productionInformationRequest.LastUpdatedBy,
                IsActive = productionInformationRequest.IsActive
            };

            await _productionInformationsService.CreateAsync(productionInformation);

            var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationuri = baseurl + "/" + ApiRoutes.ProductionInformations.Get.Replace("{productionInformationId}", productionInformation.Id.ToString());

            var response = ProdInfoToProdInfoResponse(productionInformation);
            return Created(locationuri, response);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete(ApiRoutes.ProductionInformations.Delete)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _productionInformationsService.DeleteAsync(await _productionInformationsService.GetByIdAsync(id));
            if (deleted)
                return NoContent();

            return NotFound();
        }

        public static ProductionInformation ProdInfoToProdInfoResponse(ProductionInformation prodInfo)
        {
            return new ProductionInformation
            {
                Id = prodInfo.Id,
                HouseId = prodInfo.HouseId,
                House = prodInfo.House,
                Rents = prodInfo.Rents,
                Customer = prodInfo.Customer,
                ExteriorWalls = prodInfo.ExteriorWalls,
                Ventilation = prodInfo.Ventilation,
                Note = prodInfo.Note,
                ProductionPrice = prodInfo.ProductionPrice,
                AdditionalCosts = prodInfo.AdditionalCosts,
                LastUpdatedBy = prodInfo.LastUpdatedBy,
                LastUpdatedDate = prodInfo.LastUpdatedDate,
                IsActive = prodInfo.IsActive
            };
        }
    }
}