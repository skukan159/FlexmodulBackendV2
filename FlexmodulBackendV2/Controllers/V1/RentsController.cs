﻿using System;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.Requests;
using FlexmodulBackendV2.Contracts.V1.Requests.Rent;
using FlexmodulBackendV2.Contracts.V1.Responses;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlexmodulBackendV2.Controllers.V1
{
    [EnableCors]
    [ApiController]
    public class RentsController : Controller
    {
        private readonly IRentsService _rentsService;

        public RentsController(IRentsService rentsService)
        {
            _rentsService = rentsService;
        }

        [HttpGet(ApiRoutes.Rents.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var rent = await _rentsService.GetRentsAsync();
            var rentResponses = rent.Select(RentToRentResponse).ToList();
            return Ok(rentResponses);
        }
        
         [HttpGet(ApiRoutes.Rents.Get)]
         public async Task<IActionResult> GetRent([FromRoute] Guid rentId)
         {
             var rent = await _rentsService.GetRentByIdAsync(rentId);
             if (rent == null)
                 return NotFound();
             return base.Ok(RentToRentResponse(rent));
         }

        [HttpPut(ApiRoutes.Rents.Update)]
          public async Task<IActionResult> Update([FromRoute] Guid rentId, [FromBody] UpdateRentRequest request)
          {
              var rent = await _rentsService.GetRentByIdAsync(rentId);
              rent.ProductionInformationId = request.ProductionInformationId;
              rent.StartDate = request.StartDate;
              rent.EndDate = request.EndDate;
              rent.InsurancePrice = request.InsurancePrice;
              rent.RentPrice = request.RentPrice;
 
              var updated = await _rentsService.UpdateRentAsync(rent);
              if (updated)
                  return Ok(RentToRentResponse(rent));
              return NotFound();
 
          }
 
          [HttpPost(ApiRoutes.Rents.Create)]
          public async Task<IActionResult> Create([FromBody] CreateRentRequest rentRequest)
          {
 
              var rent = new Rent
              {
                  ProductionInformationId = rentRequest.ProductionInformationId,
                  StartDate = rentRequest.StartDate,
                  EndDate = rentRequest.EndDate,
                  InsurancePrice = rentRequest.InsurancePrice,
                  RentPrice = rentRequest.RentPrice
              };
 
              await _rentsService.CreateRentAsync(rent);
 
              var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
              var locationuri = baseurl + "/" + ApiRoutes.Rents.Get.Replace("{rentId}", rent.Id.ToString());
 
              var response = RentToRentResponse(rent);
              return Created(locationuri, response);
          }
 
          [HttpDelete(ApiRoutes.Rents.Delete)]
          public async Task<ActionResult> DeleteRent([FromRoute] Guid postId)
          {
              var deleted = await _rentsService.DeleteRentAsync(postId);
              if (deleted)
                  return NoContent();
 
              return NotFound();
          }

        public static RentResponse RentToRentResponse(Rent rent)
        {
            return new RentResponse
            {
                Id = rent.Id,
                ProductionInformationId = rent.ProductionInformationId,
                StartDate = rent.StartDate,
                EndDate = rent.EndDate,
                InsurancePrice = rent.InsurancePrice,
                RentPrice = rent.RentPrice,
            };
        }

    }
}

