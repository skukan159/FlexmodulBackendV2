using System;

namespace FlexmodulBackendV2.Domain
{
    public class FmHouse : EntityBase
    {
        public Guid HouseTypeId { get; set; }  
        public FmHouseType HouseType { get; set; }
        public int SquareMeters { get; set; }
    }
}
