using System;

namespace FlexmodulBackendV2.Contracts.V1.ResponseDTO
{
    public class MaterialResponse
    {
        public Guid Id { get; set; }
        public string HouseSection { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Supplier { get; set; }
        public string Units { get; set; }
        public float PricePerUnit { get; set; }
    }
}
