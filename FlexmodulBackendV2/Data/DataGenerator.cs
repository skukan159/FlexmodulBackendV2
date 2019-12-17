using System;
using System.Collections.Generic;
using System.Linq;
using FlexmodulBackendV2.Domain;
using Microsoft.AspNetCore.Identity;

namespace FlexmodulBackendV2.Data
{
    public static class DataGenerator
    {
        public static IEnumerable<Rent> GenerateRents
            (IReadOnlyCollection<ProductionInformation> productionInformations, FmHouse houseType1, FmHouse houseType2, FmHouse houseType3)
        {
            return new List<Rent>
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
        }

        public static List<Customer> GenerateCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    CompanyName = "Best Company Ever",
                    CompanyTown = "Horsens",
                    CompanyStreet = "Osterled 2b",
                    CompanyPostalCode = "8700",
                    ContactNumber = "20687315",
                    ContactPerson = "Filip Skukan",
                },
                new Customer
                {
                    CompanyName = "Ceci and Friends",
                    CompanyTown = "Horsens",
                    CompanyStreet = "Vejlevej 77",
                    CompanyPostalCode = "8700",
                    ContactNumber = "20687398",
                    ContactPerson = "Ceci",
                },
                new Customer
                {
                    CompanyName = "Second best company ever",
                    CompanyTown = "Viborg",
                    CompanyStreet = "Houlkaervej 22",
                    CompanyPostalCode = "8800",
                    ContactNumber = "20687399",
                    ContactPerson = "Sergiu",
                }
            };
        }
        public static List<FmHouseType> GenerateFmHouseTypes()
        {
            return new List<FmHouseType>
            {
                new FmHouseType { HouseType = 1},
                new FmHouseType { HouseType = 2},
                new FmHouseType { HouseType = 3},
                new FmHouseType { HouseType = 4},
                new FmHouseType { HouseType = 5},
            };
        }

        public static List<FmHouse> GenerateFmHouses(List<FmHouseType> houseTypes)
        {
            return new List<FmHouse>
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
        }


        public static List<Material> GenerateMaterials()
        {
            return new List<Material>
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
        }

        public static List<MaterialOnHouseType> GenerateMaterialOnHouseTypes(List<Material> materials, List<FmHouseType> houseTypes)
        {
            return new List<MaterialOnHouseType>
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
        }

        public static List<ProductionInformation> GenerateProductionInformation
            (List<FmHouse> fmHouses, List<FmHouseType> houseTypes, List<Customer> customers, IdentityUser newUser)
        {
            return new List<ProductionInformation>
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
        }


        public static List<RentalOverview> GenerateRentalOverviews
            (List<ProductionInformation> productionInformations, FmHouse houseType1, FmHouse houseType2, FmHouse houseType3)
        {
            return new List<RentalOverview>
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
        }
    }
}
