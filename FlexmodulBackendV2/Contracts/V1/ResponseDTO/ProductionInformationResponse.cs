using System;
using System.Collections.Generic;
using FlexmodulBackendV2.Domain;
using Microsoft.AspNetCore.Identity;

namespace FlexmodulBackendV2.Contracts.V1.ResponseDTO
{
    public class ProductionInformationResponse
    {
        public Guid Id { get; set; }
        public Guid HouseId { get; set; }
        public List<Rent> Rents { get; set; }
        public Customer Customer { get; set; }
        public int? ExteriorWalls { get; set; }
        public int? Ventilation { get; set; }
        public string Note { get; set; }
        public float ProductionPrice { get; set; }
        public DateTime ProductionDate { get; set; }
        public ICollection<AdditionalCost> AdditionalCosts { get; set; }
        public IdentityUser LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
