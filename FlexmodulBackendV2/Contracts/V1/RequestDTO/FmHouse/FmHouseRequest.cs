﻿namespace FlexmodulBackendV2.Contracts.V1.RequestDTO.FmHouse
{
    public class FmHouseRequest
    {
        public Domain.FmHouseType HouseType { get; set; }
        public int SquareMeters { get; set; }
    }
}
