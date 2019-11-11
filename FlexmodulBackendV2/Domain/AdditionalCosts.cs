using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulAPI.Models
{
    public class AdditionalCosts
    {
        [ForeignKey("ProductionInformation")]
        public int AdditionalCostsId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public float Price { get; set; }
    }
}
