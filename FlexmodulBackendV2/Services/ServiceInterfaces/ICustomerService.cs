using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Services.ServiceInterfaces
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerAsync(Customer customer);
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(Guid customerId);
        Task<Customer> GetCustomerByNameAsync(string companyName);
        Task<bool> UpdateCustomerAsync(Customer customerToUpdate);
        Task<bool> DeleteCustomerAsync(Guid customerId);
    }
}
