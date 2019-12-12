using System;

namespace FlexmodulBackendV2.Contracts.V1.ResponseDTO
{
    public class AdditionalCostResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
    }
}
