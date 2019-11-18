using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Contracts.V1.Responses
{
    public class RentResponse
    {
        public Guid Id { get; set; }
        public Guid ProductionInformationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float InsurancePrice { get; set; }
        public float RentPrice { get; set; }
    }
}
