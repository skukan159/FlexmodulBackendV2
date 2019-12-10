using System;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.Requests.Customer;
using FlexmodulBackendV2.Contracts.V1.Responses;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlexmodulBackendV2.Controllers.V1
{
    [EnableCors("MyPolicy")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize(Roles = "Employee,Admin,SuperAdmin")]
        [HttpGet(ApiRoutes.Customers.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetCustomersAsync();
            var customerResponses = customers.Select(CustomerToCustomerResponse).ToList();
            return Ok(customerResponses);
        }

        [Authorize(Roles = "Employee,Admin,SuperAdmin")]
        [HttpGet(ApiRoutes.Customers.Get)]
        public async Task<IActionResult> GetCustomer([FromRoute]Guid customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
                return NotFound();
            return base.Ok(CustomerToCustomerResponse(customer));
        }

        [Authorize(Roles = "Employee,Admin,SuperAdmin")]
        [HttpGet(ApiRoutes.Customers.GetByName)]
        public async Task<IActionResult> GetCustomer([FromRoute]string companyName)
        {
            var customer = await _customerService.GetCustomerByNameAsync(companyName);
            if (customer == null)
                return NotFound();
            return base.Ok(CustomerToCustomerResponse(customer));
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut(ApiRoutes.Customers.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid customerId, [FromBody]UpdateCustomerRequest request)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            customer.CompanyName = request.CompanyName;
            customer.CompanyTown = request.CompanyTown;
            customer.CompanyStreet = request.CompanyStreet;
            customer.CompanyPostalCode = request.CompanyPostalCode;
            customer.ContactPerson = request.ContactPerson;

            var updated = await _customerService.UpdateCustomerAsync(customer);
            if (updated)
                return Ok(CustomerToCustomerResponse(customer));
            return NotFound();

        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost(ApiRoutes.Customers.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCustomerRequest customerRequest)
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

            await _customerService.CreateCustomerAsync(customer);

            var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationuri = baseurl + "/" + ApiRoutes.Customers.Get.Replace("{customerId}", customer.Id.ToString());

            var response = CustomerToCustomerResponse(customer);
            return Created(locationuri, response);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete(ApiRoutes.Customers.Delete)]
        public async Task<ActionResult> DeleteCustomer([FromRoute]Guid customerId)
        {
            var deleted = await _customerService.DeleteCustomerAsync(customerId);
            if (deleted)
                return NoContent();

            return NotFound();
        }

        public static CustomerResponse CustomerToCustomerResponse(Customer customer)
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
