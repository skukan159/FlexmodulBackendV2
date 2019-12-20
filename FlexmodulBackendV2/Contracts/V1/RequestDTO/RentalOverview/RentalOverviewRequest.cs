using System.Collections.Generic;

namespace FlexmodulBackendV2.Contracts.V1.RequestDTO.RentalOverview
{
    public class RentalOverviewRequest
    {
        public ICollection<Domain.ProductionInformation> ProductionInformation { get; set; }
        public string PurchaseStatus { get; set; }
        public string SetupAddressTown { get; set; }
        public string SetupAddressStreet { get; set; }
        public int SetupAddressPostalCode { get; set; }
        public float EstimatedPrice { get; set; }
    }
}
