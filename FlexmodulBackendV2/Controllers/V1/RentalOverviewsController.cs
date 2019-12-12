using System;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.RequestDTO.RentalOverview;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Employee,Admin,SuperAdmin")]
    public class RentalOverviewsController : Controller
    {
        private readonly IRepository<RentalOverview> _rentalOverviewsService;

        public RentalOverviewsController(IRepository<RentalOverview> rentalOverviewsService)
        {
            _rentalOverviewsService = rentalOverviewsService;
        }

        [HttpGet(ApiRoutes.RentalOverviews.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var rentalOverviews = await _rentalOverviewsService.GetAsync();
            var rentalOverviewResponses = rentalOverviews.Select(RentalOverviewToResponse).ToList();
            return Ok(rentalOverviewResponses);
        }

        [HttpGet(ApiRoutes.RentalOverviews.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid rentalOverviewId)
        {
            var rentalOverview = await _rentalOverviewsService.GetByIdAsync(rentalOverviewId);
            if (rentalOverview == null)
                return NotFound();
            return base.Ok(RentalOverviewToResponse(rentalOverview));
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut(ApiRoutes.RentalOverviews.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid rentalOverviewId, [FromBody] UpdateRentalOverviewRequest request)
        {
            var rentalOverview = await _rentalOverviewsService.GetByIdAsync(rentalOverviewId);
            rentalOverview.EstimatedPrice = request.EstimatedPrice;
            rentalOverview.ProductionInformations = request.ProductionInformations;
            rentalOverview.PurchaseStatus = request.PurchaseStatus;
            rentalOverview.SetupAddressPostalCode = request.SetupAddressPostalCode;
            rentalOverview.SetupAddressStreet = request.SetupAddressStreet;
            rentalOverview.SetupAddressTown = request.SetupAddressTown;


            var updated = await _rentalOverviewsService.UpdateAsync(rentalOverview);
            if (updated)
                return Ok(RentalOverviewToResponse(rentalOverview));
            return NotFound();

        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost(ApiRoutes.RentalOverviews.Create)]
        public async Task<IActionResult> Create([FromBody] CreateRentalOverviewRequest rentalOverviewRequest)
        {
            var rentalOverview = new RentalOverview
            {
                ProductionInformations = rentalOverviewRequest.ProductionInformation,
                EstimatedPrice = rentalOverviewRequest.EstimatedPrice,
                PurchaseStatus = rentalOverviewRequest.PurchaseStatus,
                SetupAddressPostalCode = rentalOverviewRequest.SetupAddressPostalCode,
                SetupAddressStreet = rentalOverviewRequest.SetupAddressStreet,
                SetupAddressTown = rentalOverviewRequest.SetupAddressTown
            };

            await _rentalOverviewsService.CreateAsync(rentalOverview);

            var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationuri = baseurl + "/" + ApiRoutes.RentalOverviews.Get.Replace("{rentalOverviewId}", rentalOverview.Id.ToString());

            var response = RentalOverviewToResponse(rentalOverview);
            return Created(locationuri, response);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete(ApiRoutes.RentalOverviews.Delete)]
        public async Task<ActionResult> Delete([FromRoute] Guid rentalOverviewId)
        {
            var deleted = await _rentalOverviewsService
                .DeleteAsync(await _rentalOverviewsService.GetByIdAsync(rentalOverviewId));
            if (deleted)
                return NoContent();

            return NotFound();
        }

        public static RentalOverviewResponse RentalOverviewToResponse(RentalOverview rentalOverview)
        {
            return new RentalOverviewResponse
            {
                Id = rentalOverview.Id,
                EstimatedPrice = rentalOverview.EstimatedPrice,
                ProductionInformation = rentalOverview.ProductionInformations,
                PurchaseStatus = rentalOverview.PurchaseStatus,
                SetupAddressPostalCode = rentalOverview.SetupAddressPostalCode,
                SetupAddressStreet = rentalOverview.SetupAddressStreet,
                SetupAddressTown = rentalOverview.SetupAddressTown
                
            };
        }
    }
}
