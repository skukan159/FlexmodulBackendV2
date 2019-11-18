using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Contracts.V1.Requests.FmHouseType
{
    public class UpdateFmHouseTypeRequest
    {
        public int HouseType { get; set; }
        public ICollection<Domain.MaterialOnHouseType> MaterialsOnHouse { get; set; }
    }
}
