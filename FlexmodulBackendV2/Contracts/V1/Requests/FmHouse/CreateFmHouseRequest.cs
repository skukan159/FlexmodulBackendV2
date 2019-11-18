using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulBackendV2.Contracts.V1.Requests.FmHouse
{
    public class CreateFmHouseRequest
    {
        public Domain.FmHouseType HouseType { get; set; }
        public int SquareMeters { get; set; }
    }
}
