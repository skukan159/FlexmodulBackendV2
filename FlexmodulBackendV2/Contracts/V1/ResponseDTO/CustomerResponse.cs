using System;

namespace FlexmodulBackendV2.Contracts.V1.ResponseDTO
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyTown { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyPostalCode { get; set; }
        public string ContactNumber { get; set; }
        public string ContactPerson { get; set; }
    }
}
