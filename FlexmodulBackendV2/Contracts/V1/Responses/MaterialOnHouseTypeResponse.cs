using System;

namespace FlexmodulBackendV2.Contracts.V1.Responses
{
    public class MaterialOnHouseTypeResponse
    {
        public Guid Id { get; set; }
        public Guid MaterialId { get; set; }
        public Guid FmHouseTypeId { get; set; }
        public Domain.Material Material { get; set; }
        public Domain.FmHouseType FmHouseType { get; set; }
        public int MaterialAmount { get; set; }
    }
}
