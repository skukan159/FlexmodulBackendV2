using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulAPI.Models
{
    public class FMHouse
    {
        public int FMHouseId { get; set; }
        [Required]
        public FMHouseType HouseType { get; set; }
        public int SquareMeters { get; set; }
        //public ProductionInformation CurrentProductionInfo { get; set; }
        //public ICollection<Rent> HouseRents { get; set; }
    }
}
