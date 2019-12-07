using System;
using System.Collections.Generic;
using System.Text;
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

        public DbSet<AdditionalCost> AdditionalCosts { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<FmHouse> FmHouses { get; set; }

        public DbSet<FmHouseType> FmHouseTypes { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<MaterialOnHouseType> MaterialOnHouseTypes { get; set; }

        public DbSet<ProductionInformation> ProductionInformations { get; set; }

        public DbSet<Rent> Rents { get; set; }

        public DbSet<RentalOverview> RentalOverviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rent>()
                .HasOne(r => r.ProductionInformation)
                .WithMany(pi => pi.Rents)
                .HasForeignKey(r => r.ProductionInformationId);


            // configures one-to-many relationship
            modelBuilder.Entity<FmHouse>()
                .HasOne(h => h.HouseType)
                .WithMany(ht => ht.Houses)
                .HasForeignKey(h => h.HouseTypeId);


            /*modelBuilder.Entity<FMHouse>().HasOne(h => h.CurrentProductionInfoId)
                .WithOne(pi => pi.House).HasForeignKey<ProductionInformation>(pi => pi.HouseId);*/
            //modelBuilder.Entity<MaterialOnHouseType>().HasKey(sc => new { FMHouseTypeId = sc.FmHouseTypeId, sc.MaterialId });
        }
    }
}
