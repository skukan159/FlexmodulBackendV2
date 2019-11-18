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
    public class RentalOverviewsService : IRentalOverviewsService
    {
        private readonly ApplicationDbContext _dataContext;

        public RentalOverviewsService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateRentalOverviewAsync(RentalOverview rentalOverview)
        {
            await _dataContext.AddAsync(rentalOverview);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<RentalOverview>> GetRentalOverviewsAsync()
        {
            return await _dataContext.RentalOverviews.ToListAsync();
        }

        public async Task<RentalOverview> GetRentalOverviewByIdAsync(Guid rentalOverviewId)
        {
            return await _dataContext.RentalOverviews
                .SingleOrDefaultAsync(r => r.Id == rentalOverviewId);
        }

        public async Task<bool> UpdateRentalOverviewAsync(RentalOverview rentalOverview)
        {
            _dataContext.RentalOverviews.Update(rentalOverview);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteRentalOverviewAsync(Guid rentalOverviewId)
        {
            var rentalOverview = await GetRentalOverviewByIdAsync(rentalOverviewId);

            if (rentalOverview == null)
                return false;

            _dataContext.RentalOverviews.Remove(rentalOverview);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
