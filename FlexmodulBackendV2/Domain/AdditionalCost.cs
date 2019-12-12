using System;
using System.ComponentModel.DataAnnotations;

namespace FlexmodulBackendV2.Domain
{
    public class AdditionalCost : EntityBase
    {
        public ProductionInformation ProductionInformation { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public float Price { get; set; }
    }
}
