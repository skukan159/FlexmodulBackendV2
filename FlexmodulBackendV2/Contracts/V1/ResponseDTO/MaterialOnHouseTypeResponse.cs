using System;

namespace FlexmodulBackendV2.Contracts.V1.ResponseDTO
{
    public class MaterialOnHouseTypeResponse
    {
        public Guid Id { get; set; }
        public Guid MaterialId { get; set; }
        public Guid FmHouseTypeId { get; set; }
        public int MaterialAmount { get; set; }
    }
}
