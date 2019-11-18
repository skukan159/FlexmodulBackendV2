using System;
using System.ComponentModel.DataAnnotations;

namespace FlexmodulBackendV2.Domain
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyTown { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyPostalCode { get; set; }
        public string ContactNumber { get; set; }
        public string ContactPerson { get; set; }
    }
}
