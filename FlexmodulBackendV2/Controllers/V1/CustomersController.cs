using System;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.RequestDTO.Customer;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Roles.Employee + "," + Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet(ApiRoutes.Customers.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAsync();
            var customerResponses = customers.Select(ToCustomerResponse).ToList();
            return Ok(customerResponses);
        }

        [HttpGet(ApiRoutes.Customers.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid customerId)
        {
            var customer = await _customerService.GetByIdAsync(customerId);
            if (customer == null)
                return NotFound();
            return base.Ok(ToCustomerResponse(customer));
        }

        [HttpGet(ApiRoutes.Customers.GetByName)]
        public async Task<IActionResult> Get([FromRoute]string companyName)
        {
            var customer = await _customerService.GetCustomerByNameAsync(companyName);
            if (customer == null)
                return NotFound();
            return base.Ok(ToCustomerResponse(customer));
        }

        [Authorize(Roles = Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
        [HttpPut(ApiRoutes.Customers.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid customerId, [FromBody]CustomerRequest request)
        {
            var customer = await _customerService.GetByIdAsync(customerId);
            customer.CompanyName = request.CompanyName;
            customer.CompanyTown = request.CompanyTown;
            customer.CompanyStreet = request.CompanyStreet;
            customer.CompanyPostalCode = request.CompanyPostalCode;
            customer.ContactPerson = request.ContactPerson;

            var updated = await _customerService.UpdateAsync(customer);
            if (updated)
                return Ok(ToCustomerResponse(customer));
            return NotFound();

        }

        [Authorize(Roles = Roles.AdministrativeEmployee + "," + Roles.SuperAdmin)]
        [HttpPost(ApiRoutes.Customers.Create)]
        public async Task<IActionResult> Create([FromBody] CustomerRequest customerRequest)
        {
            var customer = new Customer
            {
                CompanyName = customerRequest.CompanyName,
                CompanyTown = customerRequest.CompanyTown,
                CompanyStreet = customerRequest.CompanyStreet,
                CompanyPostalCode = customerRequest.CompanyPostalCode,
                ContactPerson = customerRequest.ContactPerson,
                ContactNumber = customerRequest.ContactNumber
            };

            await _customerService.CreateAsync(customer);

            var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationuri = baseurl + "/" + ApiRoutes.Customers.Get.Replace("{customerId}", customer.Id.ToString());

            var response = ToCustomerResponse(customer);
            return Created(locationuri, response);
        }

        [Authorize(Roles = Roles.SuperAdmin)]
        [HttpDelete(ApiRoutes.Customers.Delete)]
        public async Task<ActionResult> Delete([FromRoute]Guid customerId)
        {
            var customer = await _customerService.GetByIdAsync(customerId);
            var deleted = await _customerService.DeleteAsync(customer);
            if (deleted)
                return NoContent();

            return NotFound();
        }

        public static CustomerResponse ToCustomerResponse(Customer customer)
        {
            return new CustomerResponse
            {
                Id = customer.Id,
                CompanyName = customer.CompanyName,
                CompanyTown = customer.CompanyTown,
                CompanyStreet = customer.CompanyStreet,
                CompanyPostalCode = customer.CompanyPostalCode,
                ContactPerson = customer.ContactPerson,
                ContactNumber = customer.ContactNumber
            };
        }
    }
}
