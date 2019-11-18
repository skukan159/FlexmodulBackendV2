using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexmodulBackendV2.Domain
{
    public class MaterialOnHouseType
    {
        [Key]
        public Guid Id { get; set; }
        public Guid MaterialId { get; set; }
        public Guid FmHouseTypeId { get; set; }
        public Material Material { get; set; }
        public FmHouseType FmHouseType { get; set; }
        public int MaterialAmount { get; set; }
    }
}
