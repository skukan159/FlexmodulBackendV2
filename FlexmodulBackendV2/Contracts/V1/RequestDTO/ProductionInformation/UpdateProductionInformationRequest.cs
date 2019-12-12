using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FlexmodulBackendV2.Contracts.V1.RequestDTO.ProductionInformation
{
    public class UpdateProductionInformationRequest
    {
        public Domain.FmHouse House { get; set; }
        public List<Domain.Rent> Rents { get; set; }
        public Domain.Customer Customer { get; set; }
        public int ExteriorWalls { get; set; }
        public int Ventilation { get; set; }
        public string Note { get; set; }
        public int ProductionPrice { get; set; }
        public DateTime ProductionDate { get; set; }
        public ICollection<Domain.AdditionalCost> AdditionalCosts { get; set; }
        public IdentityUser LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
