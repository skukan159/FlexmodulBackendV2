using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulAPI.Models
{
    public class FMHouseType
    {
        public int FMHouseTypeId { get; set; }
        [Required]
        public int HouseType { get; set; }
        public ICollection<MaterialOnHouseType> MaterialsOnHouse { get; set; }
    }
}
