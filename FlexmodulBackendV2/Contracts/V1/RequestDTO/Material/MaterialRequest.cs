﻿namespace FlexmodulBackendV2.Contracts.V1.RequestDTO.Material
{
    public class MaterialRequest
    {
        public string HouseSection { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Supplier { get; set; }
        public string Units { get; set; }
        public float PricePerUnit { get; set; }
    }
}
