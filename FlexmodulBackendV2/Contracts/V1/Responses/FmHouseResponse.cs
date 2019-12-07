using System;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Contracts.V1.Responses
{
    public class FmHouseResponse
    {
        public Guid Id { get; set; }
        public FmHouseType HouseType { get; set; }
        public int SquareMeters { get; set; }
    }
}
