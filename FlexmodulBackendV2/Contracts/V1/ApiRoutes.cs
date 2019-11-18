namespace FlexmodulBackendV2.Contracts.V1
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Users
        {
            public const string GetAll = Base + "/users";
            public const string Get = Base + "/users/{userId}";
            public const string Create = Base + "/users";
            public const string Update = Base + "/users/{userId}";
            public const string Delete = Base + "/users/{userId}";
        }

        public static class Customers
        {
            public const string GetAll = Base + "/customers";
            public const string Get = Base + "/customers/{customerName}";
            public const string Create = Base + "/customers";
            public const string Update = Base + "/customers/{customerId}";
            public const string Delete = Base + "/customers/{customerName}";
        }

        public static class Rents
        {
            public const string GetAll = Base + "/rents";
            public const string Get = Base + "/rents/{rentId}";
            public const string Create = Base + "/rents";
            public const string Update = Base + "/rents/{rentId}";
            public const string Delete = Base + "/rents/{rentId}";
        }

        public static class FmHouseTypes
        {
            public const string GetAll = Base + "/fmhousetypes";
            public const string Get = Base + "/fmhousetypes/{fmHouseTypeId}";
            public const string Create = Base + "/fmhousetypes";
            public const string Update = Base + "/fmhousetypes/{fmHouseTypeId}";
            public const string Delete = Base + "/fmhousetypes/{fmHouseTypeId}";
        }

        public static class FmHouses
        {
            public const string GetAll = Base + "/fmhouses";
            public const string Get = Base + "/fmhouses/{fmHouseId}";
            public const string Create = Base + "/fmhouses";
            public const string Update = Base + "/fmhouses/{fmHouseId}";
            public const string Delete = Base + "/fmhouses/{fmHouseId}";
        }

        public static class Materials
        {
            public const string GetAll = Base + "/materials";
            public const string Get = Base + "/materials/{materialId}";
            public const string Create = Base + "/materials";
            public const string Update = Base + "/materials/{materialId}";
            public const string Delete = Base + "/materials/{materialId}";
        }

        public static class MaterialOnHouseTypes
        {
            public const string GetAll = Base + "/materialonhousetypes";
            public const string Get = Base + "/materialonhousetypes/{materialonhousetypeId}";
            public const string Create = Base + "/materialonhousetypes";
            public const string Update = Base + "/materialonhousetypes/{materialonhousetypeId}";
            public const string Delete = Base + "/materialonhousetypes/{materialonhousetypeId}";
        }

        public static class ProductionInformations
        {
            public const string GetAll = Base + "/productioninformations";
            public const string Get = Base + "/productioninformations/{productioninformationId}";
            public const string Create = Base + "/productioninformations";
            public const string Update = Base + "/productioninformations/{productioninformationId}";
            public const string Delete = Base + "/productioninformations/{productioninformationId}";
        }

        public static class RentalOverviews
        {
            public const string GetAll = Base + "/rentaloverviews";
            public const string Get = Base + "/rentaloverviews/{rentaloverviewId}";
            public const string Create = Base + "/rentaloverviews";
            public const string Update = Base + "/rentaloverviews/{rentaloverviewId}";
            public const string Delete = Base + "/rentaloverviews/{rentaloverviewId}";
        }

        public static class AdditionalCosts
        {
            public const string GetAll = Base + "/additionalcosts";
            public const string Get = Base + "/additionalcosts/{additionalcostId}";
            public const string Create = Base + "/additionalcosts";
            public const string Update = Base + "/additionalcosts/{additionalcostId}";
            public const string Delete = Base + "/additionalcosts/{additionalcostId}";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string Refresh = Base + "/identity/refresh";
        }


    }
}
