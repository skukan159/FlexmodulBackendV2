using System;

namespace FlexmodulBackendV2.Contracts.V1.RequestDTO.MaterialOnHouseType
{
    public class MaterialOnHouseTypeRequest
    {
        public Guid MaterialId { get; set; }
        public Guid FmHouseTypeId { get; set; }
        public int MaterialAmount { get; set; }
    }
}
