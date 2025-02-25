﻿using System;
using System.Collections.Generic;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Contracts.V1.ResponseDTO
{
    public class FmHouseResponse
    {
        public Guid Id { get; set; }
        public int HouseType { get; set; }
        public int SquareMeters { get; set; }
        public ICollection<MaterialOnHouseType> MaterialsOnHouse { get; set; }
    }
}
