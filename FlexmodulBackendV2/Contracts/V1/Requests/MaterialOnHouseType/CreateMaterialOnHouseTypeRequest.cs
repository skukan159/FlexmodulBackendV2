﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulBackendV2.Contracts.V1.Requests.MaterialOnHouseType
{
    public class CreateMaterialOnHouseTypeRequest
    {
        public Guid MaterialId { get; set; }
        public Guid FmHouseTypeId { get; set; }
        public Domain.Material Material { get; set; }
        public Domain.FmHouseType FmHouseType { get; set; }
        public int MaterialAmount { get; set; }
    }
}