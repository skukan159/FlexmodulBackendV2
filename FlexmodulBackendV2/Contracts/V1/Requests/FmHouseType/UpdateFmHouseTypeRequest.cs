using System.Collections.Generic;

namespace FlexmodulBackendV2.Contracts.V1.Requests.FmHouseType
{
    public class UpdateFmHouseTypeRequest
    {
        public int HouseType { get; set; }
        public ICollection<Domain.MaterialOnHouseType> MaterialsOnHouse { get; set; }
    }
}
