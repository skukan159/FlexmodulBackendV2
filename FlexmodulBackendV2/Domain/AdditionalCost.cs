using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexmodulBackendV2.Domain
{
    public class AdditionalCost
    {
        [Key]
        [ForeignKey("ProductionInformation")]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public float Price { get; set; }
    }
}
