using System;

namespace FlexmodulBackendV2.Contracts.V1.Requests.Rent
{
    public class UpdateRentRequest
    {
        public Guid ProductionInformationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float InsurancePrice { get; set; }
        public float RentPrice { get; set; }
    }
}
