using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlexmodulBackendV2.Domain
{
    public class RentalOverview
    {
        public enum PurchaseStatuses
        {
            Leased,
            ContractNotInitiated,
            Terminated,
            Stock,
            SoldOut
        }
        [Key]
        public Guid Id { get; set; }
        public ICollection<FmHouse> RentedHouses { get; set; }
        public ICollection<ProductionInformation> ProductionInformation { get; set; }
        public PurchaseStatuses PurchaseStatus { get; set; }
        public string SetupAddressTown { get; set; }
        public string SetupAddressStreet { get; set; }
        public int SetupAddressPostalCode { get; set; }
        public float EstimatedPrice { get; set; }
    }
}
