using System;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.RequestDTO.AdditionalCost;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdditionalCostsController : Controller
    {
        private readonly IRepository<AdditionalCost> _additionalCostsService;

        public AdditionalCostsController(IRepository<AdditionalCost> additionalCostsService)
        {
            _additionalCostsService = additionalCostsService;
        }

        [HttpGet(ApiRoutes.AdditionalCosts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var additionalCosts = await _additionalCostsService.GetAsync();
            var additionalCostsResponses = additionalCosts.Select(AdditionalCostToAdditionalCostResponse).ToList();
            return Ok(additionalCostsResponses);
        }

        [HttpGet(ApiRoutes.AdditionalCosts.Get)]
        public async Task<IActionResult> GetCustomer([FromRoute]Guid customerId)
        {
            var additionalCost = await _additionalCostsService.GetByIdAsync(customerId);
            if (additionalCost == null)
                return NotFound();
            return base.Ok(AdditionalCostToAdditionalCostResponse(additionalCost));
        }

        [HttpPut(ApiRoutes.AdditionalCosts.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid additionalCostId, [FromBody]UpdateAdditionalCostRequest request)
        {
            var additionalCost = await _additionalCostsService.GetByIdAsync(additionalCostId);
            additionalCost.Description = request.Description;
            additionalCost.Price = request.Price;
            additionalCost.Date = request.Date;

            var updated = await _additionalCostsService.UpdateAsync(additionalCost);
            if (updated)
                return Ok(AdditionalCostToAdditionalCostResponse(additionalCost));
            return NotFound();

        }

        [HttpPost(ApiRoutes.AdditionalCosts.Create)]
        public async Task<IActionResult> Create([FromBody] CreateAdditionalCostRequest customerRequest)
        {
            var additionalCost = new AdditionalCost
            {
                Description = customerRequest.Description,
                Price = customerRequest.Price,
                Date = customerRequest.Date
            };

            await _additionalCostsService.CreateAsync(additionalCost);

            var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationuri = baseurl + "/" + ApiRoutes.AdditionalCosts.Get.Replace("{additionalCostId}", additionalCost.Id.ToString());

            var response = AdditionalCostToAdditionalCostResponse(additionalCost);
            return Created(locationuri, response);
        }

        [HttpDelete(ApiRoutes.AdditionalCosts.Delete)]
        public async Task<ActionResult> DeleteCustomer([FromRoute]Guid additionalCostId)
        {
            var additionalCost = await _additionalCostsService.GetByIdAsync(additionalCostId);
            var deleted = await _additionalCostsService.DeleteAsync(additionalCost);
            if (deleted)
                return NoContent();

            return NotFound();
        }

        public static AdditionalCostResponse AdditionalCostToAdditionalCostResponse(AdditionalCost additionalCost)
        {
            return new AdditionalCostResponse
            {
                Id = additionalCost.Id,
                Description = additionalCost.Description,
                Date = additionalCost.Date,
                Price = additionalCost.Price
            };
        }
    }
}