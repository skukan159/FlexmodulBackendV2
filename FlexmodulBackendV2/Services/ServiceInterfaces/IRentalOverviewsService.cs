using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Services.ServiceInterfaces
{
    public interface IRentalOverviewsService
    {
        Task<bool> CreateRentalOverviewAsync(RentalOverview rentalOverview);
        Task<List<RentalOverview>> GetRentalOverviewsAsync();
        Task<RentalOverview> GetRentalOverviewByIdAsync(Guid rentalOverviewId);
        Task<bool> UpdateRentalOverviewAsync(RentalOverview rentalOverview);
        Task<bool> DeleteRentalOverviewAsync(Guid rentalOverviewId);
    }
}
