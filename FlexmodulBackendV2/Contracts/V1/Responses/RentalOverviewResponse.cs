using System;
using System.Collections.Generic;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Contracts.V1.Responses
{
    public class RentalOverviewResponse
    {
        public Guid Id { get; set; }
        public ICollection<FmHouse> RentedHouses { get; set; }
        public ICollection<ProductionInformation> ProductionInformation { get; set; }
        public RentalOverview.PurchaseStatuses PurchaseStatus { get; set; }
        public string SetupAddressTown { get; set; }
        public string SetupAddressStreet { get; set; }
        public int SetupAddressPostalCode { get; set; }
        public float EstimatedPrice { get; set; }
    }
}
