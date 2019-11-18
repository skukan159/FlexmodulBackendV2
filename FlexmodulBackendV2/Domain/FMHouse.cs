using System;
using System.ComponentModel.DataAnnotations;

namespace FlexmodulBackendV2.Domain
{
    public class FmHouse
    {   
        [Key]
        public Guid Id { get; set; }
        [Required]
        public FmHouseType HouseType { get; set; }
        public int SquareMeters { get; set; }
        //public ProductionInformation CurrentProductionInfo { get; set; }
        //public ICollection<Rent> HouseRents { get; set; }
    }
}
