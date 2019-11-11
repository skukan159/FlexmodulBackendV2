using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulAPI.Models
{
    public class MaterialOnHouseType
    {
        [Key, Column(Order = 1)]
        public int MaterialId { get; set; }
        [Key, Column(Order = 2)]
        public int FMHouseTypeId { get; set; }
        public Material Material { get; set; }
        public FMHouseType FMHouseType { get; set; }
        public int MaterialAmount { get; set; }
    }
}
