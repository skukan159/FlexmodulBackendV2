using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace FlexmodulBackendV2.Services
{
    public class CustomerService : Repository<Customer>, ICustomerService
    {
        public CustomerService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Customer> GetCustomerByNameAsync(string companyName)
        {
            return await DbContext.Customers
                .SingleOrDefaultAsync(c => c.CompanyName == companyName);
        }

    }
}