using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulBackendV2.Contracts.V1.Responses
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyTown { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyPostalCode { get; set; }
        public string ContactNumber { get; set; }
        public string ContactPerson { get; set; }
    }
}
