using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlexmodulBackendV2.Domain
{
    public class FmHouseType : EntityBase
    {
        [Required]
        public int HouseType { get; set; }
        public ICollection<MaterialOnHouseType> MaterialsOnHouse { get; set; }
        public ICollection<FmHouse> Houses { get; set; }
    }
}
