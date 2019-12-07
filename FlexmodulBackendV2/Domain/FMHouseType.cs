using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexmodulBackendV2.Domain
{
    public class FmHouseType
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public int HouseType { get; set; }
        public ICollection<MaterialOnHouseType> MaterialsOnHouse { get; set; }
        public ICollection<FmHouse> Houses { get; set; }
    }
}
