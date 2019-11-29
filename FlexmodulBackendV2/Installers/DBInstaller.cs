using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Services;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlexmodulBackendV2.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAdditionalCostsService, AdditionalCostsService>();
            services.AddScoped<IFmHouseTypesService, FmHouseTypeService>();
            services.AddScoped<IFmHousesService, FmHouseService>();
            services.AddScoped<IMaterialsService, MaterialService>();
            services.AddScoped<IProductionInformationsService, ProductionInformationService>();
            services.AddScoped<IRentalOverviewsService, RentalOverviewService>();
            services.AddScoped<IRentsService, RentService>();
            services.AddScoped<IMaterialOnHouseTypesService, MaterialOnHouseTypesService>();
        }
    }
}
