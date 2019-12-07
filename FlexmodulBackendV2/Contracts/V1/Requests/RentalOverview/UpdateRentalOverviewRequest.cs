﻿using System.Collections.Generic;

namespace FlexmodulBackendV2.Contracts.V1.Requests.RentalOverview
{
    public class UpdateRentalOverviewRequest
    {
        public ICollection<Domain.FmHouse> RentedHouses { get; set; }
        public ICollection<Domain.ProductionInformation> ProductionInformation { get; set; }
        public Domain.RentalOverview.PurchaseStatuses PurchaseStatus { get; set; }
        public string SetupAddressTown { get; set; }
        public string SetupAddressStreet { get; set; }
        public int SetupAddressPostalCode { get; set; }
        public float EstimatedPrice { get; set; }
    }
}
