﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.Requests.Material;
using FlexmodulBackendV2.Contracts.V1.Requests.RentalOverview;
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
    public class RentalOverviewsController : Controller
    {
        private readonly IRentalOverviewsService _rentalOverviewsService;

        public RentalOverviewsController(IRentalOverviewsService rentalOverviewsService)
        {
            _rentalOverviewsService = rentalOverviewsService;
        }

        [HttpGet(ApiRoutes.RentalOverviews.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var rentalOverviews = await _rentalOverviewsService.GetRentalOverviewsAsync();
            var rentalOverviewResponses = rentalOverviews.Select(RentalOverviewToResponse).ToList();
            return Ok(rentalOverviewResponses);
        }

        [HttpGet(ApiRoutes.RentalOverviews.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid rentalOverviewId)
        {
            var rentalOverview = await _rentalOverviewsService.GetRentalOverviewByIdAsync(rentalOverviewId);
            if (rentalOverview == null)
                return NotFound();
            return base.Ok(RentalOverviewToResponse(rentalOverview));
        }

        [HttpPut(ApiRoutes.RentalOverviews.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid rentalOverviewId, [FromBody] UpdateRentalOverviewRequest request)
        {
            var rentalOverview = await _rentalOverviewsService.GetRentalOverviewByIdAsync(rentalOverviewId);
            rentalOverview.EstimatedPrice = request.EstimatedPrice;
            rentalOverview.ProductionInformation = request.ProductionInformation;
            rentalOverview.PurchaseStatus = request.PurchaseStatus;
            rentalOverview.RentedHouses = request.RentedHouses;
            rentalOverview.SetupAddressPostalCode = request.SetupAddressPostalCode;
            rentalOverview.SetupAddressStreet = request.SetupAddressStreet;
            rentalOverview.SetupAddressTown = request.SetupAddressTown;


            var updated = await _rentalOverviewsService.UpdateRentalOverviewAsync(rentalOverview);
            if (updated)
                return Ok(RentalOverviewToResponse(rentalOverview));
            return NotFound();

        }

        [HttpPost(ApiRoutes.RentalOverviews.Create)]
        public async Task<IActionResult> Create([FromBody] CreateRentalOverviewRequest rentalOverviewRequest)
        {
            var rentalOverview = new RentalOverview
            {
                ProductionInformation = rentalOverviewRequest.ProductionInformation,
                EstimatedPrice = rentalOverviewRequest.EstimatedPrice,
                PurchaseStatus = rentalOverviewRequest.PurchaseStatus,
                RentedHouses = rentalOverviewRequest.RentedHouses,
                SetupAddressPostalCode = rentalOverviewRequest.SetupAddressPostalCode,
                SetupAddressStreet = rentalOverviewRequest.SetupAddressStreet,
                SetupAddressTown = rentalOverviewRequest.SetupAddressTown
            };

            await _rentalOverviewsService.CreateRentalOverviewAsync(rentalOverview);

            var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationuri = baseurl + "/" + ApiRoutes.RentalOverviews.Get.Replace("{rentalOverviewId}", rentalOverview.Id.ToString());

            var response = RentalOverviewToResponse(rentalOverview);
            return Created(locationuri, response);
        }

        [HttpDelete(ApiRoutes.RentalOverviews.Delete)]
        public async Task<ActionResult> Delete([FromRoute] Guid rentalOverviewId)
        {
            var deleted = await _rentalOverviewsService.DeleteRentalOverviewAsync(rentalOverviewId);
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
                ProductionInformation = rentalOverview.ProductionInformation,
                PurchaseStatus = rentalOverview.PurchaseStatus,
                RentedHouses = rentalOverview.RentedHouses,
                SetupAddressPostalCode = rentalOverview.SetupAddressPostalCode,
                SetupAddressStreet = rentalOverview.SetupAddressStreet,
                SetupAddressTown = rentalOverview.SetupAddressTown
                
            };
        }
    }
}