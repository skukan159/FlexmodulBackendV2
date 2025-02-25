﻿using System;

namespace FlexmodulBackendV2.Domain
{
    public class Rent : EntityBase
    {
        public Guid ProductionInformationId { get; set; }
        public ProductionInformation ProductionInformation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float InsurancePrice { get; set; }
        public float RentPrice { get; set; }
    }
}
