using System;
using System.ComponentModel.DataAnnotations;

namespace FlexmodulBackendV2.Domain
{
    public class AdditionalCost
    {
        [Key]
        public Guid Id { get; set; }
        public ProductionInformation ProductionInformation { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public float Price { get; set; }
    }
}
