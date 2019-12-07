using System;
using System.Linq;
using FlexmodulBackendV2.Domain;
using Microsoft.AspNetCore.Identity;

namespace FlexmodulBackendV2.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //context.Database.EnsureCreated();

            if (context.FmHouseTypes.Any())
            {
                return;   // DB has been seeded
            }

            var newUser = new IdentityUser
            {
                Email = "admin@test.com",
                UserName = "admin@test.com"
            };

            var customers = new[]
            {
                new Customer {
                    CompanyName = "Best Company Ever",
                    CompanyTown = "Horsens",
                    CompanyStreet = "Osterled 2b",
                    CompanyPostalCode = "8700",
                    ContactNumber = "20687315",
                    ContactPerson = "Filip Skukan",
                },
                new Customer {
                    CompanyName = "Ceci and Friends",
                    CompanyTown = "Horsens",
                    CompanyStreet = "Vejlevej 77",
                    CompanyPostalCode = "8700",
                    ContactNumber = "20687398",
                    ContactPerson = "Ceci",
                },
                new Customer {
                    CompanyName = "Second best company ever",
                    CompanyTown = "Viborg",
                    CompanyStreet = "Houlkaervej 22",
                    CompanyPostalCode = "8800",
                    ContactNumber = "20687399",
                    ContactPerson = "Sergiu",
                },
            };

            foreach (var customer in customers)
            {
                context.Customers.Add(customer);
            }

            context.SaveChanges();


            var houseTypes = new[]
            {
                new FmHouseType { HouseType = 1},
                new FmHouseType { HouseType = 2},
                new FmHouseType { HouseType = 3},
                new FmHouseType { HouseType = 4},
                new FmHouseType { HouseType = 5},
            };

            foreach (var s in houseTypes)
            {
                context.FmHouseTypes.Add(s);
            }
            context.SaveChanges();

            var materials = new[]
            {
                new Material { 
                    HouseSection = Material.HouseSections.Floor,
                    Category = "240 - Konstruktion", Name = "12 MM OSB",
                    Units = "Niveauforskel",PricePerUnit = 28.25f },
                new Material {
                    HouseSection = Material.HouseSections.OuterWalls,
                    Category = "240 - Konstruktionstrae", Name = "45x195 mm regel",
                    PricePerUnit = 16.75f},
                new Material {
                    HouseSection = Material.HouseSections.DoorsWindows,
                    Category = "Kastrup", Name = "330 x 2118",Units = "3-lags Trae/Alu",
                    Supplier="Kastrup", PricePerUnit = 16.75f },

            };

            foreach (var m in materials)
            {
                context.Materials.Add(m);
            }
            context.SaveChanges();

            var materialsOnHouses = new[]
            {
                new MaterialOnHouseType { 
                    MaterialId = materials.Single( s => s.Name == "12 MM OSB").Id,
                    FmHouseTypeId = houseTypes.Single( h => h.HouseType == 1).Id
                },
                new MaterialOnHouseType {
                    MaterialId = materials.Single( s => s.Name == "12 MM OSB").Id,
                    FmHouseTypeId = houseTypes.Single( h => h.HouseType ==2).Id
                },
                new MaterialOnHouseType {
                    MaterialId = materials.Single( s => s.Name == "12 MM OSB").Id,
                    FmHouseTypeId = houseTypes.Single( h => h.HouseType == 3).Id
                },
                new MaterialOnHouseType {
                    MaterialId = materials.Single( s => s.Name == "12 MM OSB").Id,
                    FmHouseTypeId = houseTypes.Single( h => h.HouseType == 4).Id
                },
                new MaterialOnHouseType {
                    MaterialId = materials.Single( s => s.Name == "12 MM OSB").Id,
                    FmHouseTypeId = houseTypes.Single( h => h.HouseType == 5).Id
                },

                new MaterialOnHouseType {
                    MaterialId = materials.Single( s => s.Name == "45x195 mm regel").Id,
                    FmHouseTypeId = houseTypes.Single( h => h.HouseType == 5).Id
                },

            };

             foreach (var mat in materialsOnHouses)
             {
                 context.MaterialOnHouseTypes.Add(mat);
             }
             context.SaveChanges();

            var fmHouses = new[]
            {
                new FmHouse {
                    HouseType = houseTypes.Single( ht => ht.HouseType == 1),
                    SquareMeters = 23,
                },
                new FmHouse {
                    HouseType = houseTypes.Single( ht => ht.HouseType == 2),
                    SquareMeters = 33,
                },
                new FmHouse {
                    HouseType = houseTypes.Single( ht => ht.HouseType == 3),
                    SquareMeters = 50,
                },

            };

            foreach (var h in fmHouses)
            {
                context.FmHouses.Add(h);
            }
            context.SaveChanges();

            var productionInformations = new[]
            {
                new ProductionInformation {
                    House = fmHouses.Single( h => h.HouseType == houseTypes.Single( ht => ht.HouseType == 1) ),
                    Customer = customers.Single( c => c.CompanyName == "Best Company Ever"),
                    ProductionPrice = 1000000,
                    ProductionDate = DateTime.Now,
                    LastUpdatedDate = DateTime.Now,
                    LastUpdatedBy = newUser,

                },
                new ProductionInformation {
                    House = fmHouses.Single( h => h.HouseType == houseTypes.Single( ht => ht.HouseType == 2) ),
                    Customer = customers.Single( c => c.CompanyName == "Ceci and Friends"),
                    ProductionPrice = 1000000,
                    ProductionDate = DateTime.Now,
                    LastUpdatedDate = DateTime.Now,
                    LastUpdatedBy = newUser,

                },
                new ProductionInformation {
                    House = fmHouses.Single( h => h.HouseType == houseTypes.Single( ht => ht.HouseType == 3) ),
                    Customer = customers.Single( c => c.CompanyName == "Second best company ever"),
                    ProductionPrice = 1000000,
                    ProductionDate = DateTime.Now,
                    LastUpdatedDate = DateTime.Now,
                    LastUpdatedBy = newUser,

                },
            };

            foreach (var pi in productionInformations)
            {
                context.ProductionInformations.Add(pi);
            }
            context.SaveChanges();

            //Update houses with production information
            var houseType1 = fmHouses.Single(h => h.HouseType == houseTypes.Single(ht => ht.HouseType == 1));
            var houseType2 = fmHouses.Single(h => h.HouseType == houseTypes.Single(ht => ht.HouseType == 2));
            var houseType3 = fmHouses.Single(h => h.HouseType == houseTypes.Single(ht => ht.HouseType == 3));
            /*
            var updatedFMHouses = new FMHouse[]
            {
                new FMHouse
                {
                    FMHouseId = houseType1.FMHouseId,
                    SquareMeters = houseType1.SquareMeters,
                    CurrentProductionInfo = productionInformations.Single(
                        pi => pi.House == houseType1
                   ),
                },
                new FMHouse
                {
                    FMHouseId = houseType2.FMHouseId,
                    SquareMeters = houseType2.SquareMeters,
                    CurrentProductionInfo = productionInformations.Single(
                        pi => pi.House == houseType2
                   ),
                },
                new FMHouse
                {
                    FMHouseId = houseType3.FMHouseId,
                    SquareMeters = houseType3.SquareMeters,
                    CurrentProductionInfo = productionInformations.Single(
                        pi => pi.House == houseType3
                   ),
                },
            };

            foreach(FMHouse h in updatedFMHouses)
            {
                context.Entry(h).State = EntityState.Modified;
                //context.FMHouse.Update(h);
            }
            context.SaveChanges();*/

            var rents = new[]
            {
                new Rent {
                    ProductionInformation = productionInformations.Single( pi => pi.House == houseType1),
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    RentPrice = 10000f,
                },
                new Rent {
                    ProductionInformation = productionInformations.Single( pi => pi.House == houseType2),
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    RentPrice = 15000f,
                },
                new Rent {
                    ProductionInformation = productionInformations.Single( pi => pi.House == houseType3),
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    RentPrice = 9000f,
                },

            };

            foreach (var r in rents)
            {
                context.Rents.Add(r);
            }
            context.SaveChanges();

            var rentalOverviews = new[]
           {
                new RentalOverview {
                    ProductionInformations = productionInformations.Where( pi => pi.House == houseType1).ToList(),
                    PurchaseStatus = RentalOverview.PurchaseStatuses.ContractNotInitiated,
                    SetupAddressTown = "Horsens",
                    SetupAddressStreet = "SomeStreet 23",
                    SetupAddressPostalCode = 8700,
                    EstimatedPrice = 12000f,
                },
                new RentalOverview {
                    ProductionInformations = productionInformations.Where( pi => pi.House == houseType2).ToList(),
                    PurchaseStatus = RentalOverview.PurchaseStatuses.Leased,
                    SetupAddressTown = "Vejle",
                    SetupAddressStreet = "SomeStreet 44",
                    SetupAddressPostalCode = 8600,
                },
                new RentalOverview {
                    ProductionInformations = productionInformations.Where( pi => pi.House == houseType3).ToList(),
                    PurchaseStatus = RentalOverview.PurchaseStatuses.Terminated,
                    SetupAddressTown = "Horsens",
                    SetupAddressStreet = "SomeStreet 55",
                    SetupAddressPostalCode = 8700,
                    EstimatedPrice = 33000f,
                },

           };

            foreach (var ro in rentalOverviews)
            {
                context.RentalOverviews.Add(ro);
            }
            context.SaveChanges();

        }
    }
}
