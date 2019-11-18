using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace FlexmodulBackendV2.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _dataContext;

        public CustomerService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            await _dataContext.AddAsync(customer);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _dataContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
        {
            return await _dataContext.Customers
                .SingleOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customerToUpdate)
        {
            _dataContext.Customers.Update(customerToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteCustomerAsync(Guid customerId)
        {
            var customer = await GetCustomerByIdAsync(customerId);

            if (customer == null)
                return false;

            _dataContext.Customers.Remove(customer);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

    }
}
