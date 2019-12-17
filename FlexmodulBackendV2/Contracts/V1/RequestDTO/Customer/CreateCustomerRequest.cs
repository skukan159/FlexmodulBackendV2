﻿namespace FlexmodulBackendV2.Contracts.V1.RequestDTO.Customer
{
    public class CustomerRequest
    {
        public string CompanyName { get; set; }
        public string CompanyTown { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyPostalCode { get; set; }
        public string ContactNumber { get; set; }
        public string ContactPerson { get; set; }
    }
}
