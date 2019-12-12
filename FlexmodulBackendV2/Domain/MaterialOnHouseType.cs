using System;
using System.ComponentModel.DataAnnotations;

namespace FlexmodulBackendV2.Domain
{
    public class MaterialOnHouseType : EntityBase
    {
        public Guid MaterialId { get; set; }
        public Guid FmHouseTypeId { get; set; }
        public Material Material { get; set; }
        public FmHouseType FmHouseType { get; set; }
        public int MaterialAmount { get; set; }
    }
}
