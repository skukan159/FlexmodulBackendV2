﻿namespace FlexmodulBackendV2.Contracts.V1.Requests.FmHouse
{
    public class UpdateFmHouseRequest
    {
        public Domain.FmHouseType HouseType { get; set; }
        public int SquareMeters { get; set; }
    }
}
