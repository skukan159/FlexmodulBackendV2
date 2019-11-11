using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulAPI.Models
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

        public int RentalOverviewId { get; set; }
        public ICollection<FMHouse> RentedHouses { get; set; }
        public ICollection<ProductionInformation> ProductionInformation { get; set; }
        public PurchaseStatuses PurchaseStatus { get; set; }
        public string SetupAddressTown { get; set; }
        public string SetupAddressStreet { get; set; }
        public int SetupAddressPostalCode { get; set; }
        public float EstimatedPrice { get; set; }
    }
}
