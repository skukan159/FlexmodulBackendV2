using System;
using System.Collections.Generic;
using System.Text;
using FlexmodulAPI.Models;
using FlexmodulBackendV2.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlexmodulBackendV2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<FlexmodulAPI.Models.AdditionalCosts> AdditionalCosts { get; set; }

        public DbSet<FlexmodulAPI.Models.Customer> Customer { get; set; }

        public DbSet<FlexmodulAPI.Models.FMHouse> FMHouse { get; set; }

        public DbSet<FlexmodulAPI.Models.FMHouseType> FMHouseType { get; set; }

        public DbSet<FlexmodulAPI.Models.Material> Material { get; set; }

        public DbSet<FlexmodulAPI.Models.MaterialOnHouseType> MaterialOnHouseType { get; set; }

        public DbSet<FlexmodulAPI.Models.ProductionInformation> ProductionInformation { get; set; }

        public DbSet<FlexmodulAPI.Models.Rent> Rent { get; set; }

        public DbSet<FlexmodulAPI.Models.RentalOverview> RentalOverview { get; set; }

        public DbSet<FlexmodulAPI.Models.User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*modelBuilder.Entity<FMHouse>().HasOne(h => h.CurrentProductionInfoId)
                .WithOne(pi => pi.House).HasForeignKey<ProductionInformation>(pi => pi.HouseId);*/
            modelBuilder.Entity<MaterialOnHouseType>().HasKey(sc => new { sc.FMHouseTypeId, sc.MaterialId });
        }
    }
}
