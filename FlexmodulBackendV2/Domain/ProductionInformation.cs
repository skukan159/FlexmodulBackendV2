using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulAPI.Models
{
    public class ProductionInformation
    {
        public int ProductionInformationId { get; set; }
        public FMHouse House { get; set; }
        public int HouseId { get; set; }
        [Required]
        public Customer Customer { get; set; }
        public int? ExteriorWalls { get; set; }
        public int? Ventilation { get; set; }
        public string Note { get; set; }
        [Required]
        public int ProductionPrice { get; set; }
        [Required]
        public DateTime ProductionDate { get; set; }
        public ICollection<AdditionalCosts> AdditionalCosts { get; set; }
        [Required]
        public User LastUpdatedBy { get; set; }
        [Required]
        public DateTime LastUpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
