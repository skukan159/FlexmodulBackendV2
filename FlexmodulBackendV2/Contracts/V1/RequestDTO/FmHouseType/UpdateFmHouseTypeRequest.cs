using System.Collections.Generic;

namespace FlexmodulBackendV2.Contracts.V1.RequestDTO.FmHouseType
{
    public class UpdateFmHouseTypeRequest
    {
        public int HouseType { get; set; }
        public ICollection<Domain.MaterialOnHouseType> MaterialsOnHouse { get; set; }
    }
}
