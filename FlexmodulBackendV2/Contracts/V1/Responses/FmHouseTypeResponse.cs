﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Contracts.V1.Responses
{
    public class FmHouseTypeResponse
    {
        public Guid Id { get; set; }
        public int HouseType { get; set; }
        public ICollection<MaterialOnHouseType> MaterialsOnHouse { get; set; }
    }
}