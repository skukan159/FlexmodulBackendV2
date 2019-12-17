namespace FlexmodulBackendV2.Domain
{
    public class Customer : EntityBase
    {
        public string CompanyName { get; set; }
        public string CompanyTown { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyPostalCode { get; set; }
        public string ContactNumber { get; set; }
        public string ContactPerson { get; set; }
    }
}
