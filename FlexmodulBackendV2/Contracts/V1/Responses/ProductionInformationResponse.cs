﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Contracts.V1.Responses
{
    public class ProductionInformationResponse
    {
        public Guid Id { get; set; }
        public FmHouse House { get; set; }
        public List<Rent> Rents { get; set; }
        public Customer Customer { get; set; }
        public int? ExteriorWalls { get; set; }
        public int? Ventilation { get; set; }
        public string Note { get; set; }
        public int ProductionPrice { get; set; }
        public DateTime ProductionDate { get; set; }
        public ICollection<AdditionalCost> AdditionalCosts { get; set; }
        public User LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}