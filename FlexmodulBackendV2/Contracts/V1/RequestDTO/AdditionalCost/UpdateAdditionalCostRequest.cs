using System;

namespace FlexmodulBackendV2.Contracts.V1.RequestDTO.AdditionalCost
{
    public class UpdateAdditionalCostRequest
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
    }
}
