using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Services.ServiceInterfaces
{
    public interface ICustomerService : IRepository<Customer>
    {
        Task<Customer> GetCustomerByNameAsync(string companyName);
    }
}
