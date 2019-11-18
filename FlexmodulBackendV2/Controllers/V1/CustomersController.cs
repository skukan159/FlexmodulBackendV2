using System;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.Requests.Customer;
using FlexmodulBackendV2.Contracts.V1.Responses;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlexmodulBackendV2.Controllers.V1
{
    [EnableCors]
    //Todo: Enable this functionality
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Route("api/[controller]")]
    //[ApiController]
    public class CustomersController : Controller//ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customers
        [HttpGet(ApiRoutes.Customers.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetCustomersAsync();
            var customerResponses = customers.Select(CustomerToCustomerResponse).ToList();
            return Ok(customerResponses);
        }

        // GET: api/Customers/5
        [HttpGet(ApiRoutes.Customers.Get)]
        public async Task<IActionResult> GetCustomer([FromRoute]Guid customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
                return NotFound();
            return base.Ok(CustomerToCustomerResponse(customer));
        }

        // PUT: api/Customers/5
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

        // POST: api/Customers
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

        // DELETE: api/Customers/5
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
