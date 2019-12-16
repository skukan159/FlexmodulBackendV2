using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
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
            services.AddScoped<IRepository<AdditionalCost>, Repository<AdditionalCost>>();
            services.AddScoped<IFmHouseTypesService, FmHouseTypeService>();
            services.AddScoped<IRepository<FmHouse>, Repository<FmHouse>>();
            services.AddScoped<IRepository<Material>, Repository<Material>>();
            services.AddScoped<IRepository<ProductionInformation>, ProductionInformationService>();
            services.AddScoped<IRepository<RentalOverview>, Repository<RentalOverview>>();
            services.AddScoped<IRepository<Rent>, Repository<Rent>>();
            services.AddScoped<IRepository<MaterialOnHouseType>, Repository<MaterialOnHouseType>>();
        }
    }
}
