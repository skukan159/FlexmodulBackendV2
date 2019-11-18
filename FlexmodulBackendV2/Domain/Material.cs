using System;
using System.ComponentModel.DataAnnotations;

namespace FlexmodulBackendV2.Domain
{
    public class Material
    {
        public enum HouseSections
        {
            IndependentWindowsDoors,
            Floor,
            OuterWalls,
            AssembleWalls,
            InnerWalls,
            DoorsWindows,
            RoofCeiling
        }

        [Key]
        public Guid Id { get; set; }
        public HouseSections HouseSection { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Supplier { get; set; }
        public string Units { get; set; }
        public float PricePerUnit { get; set; }
    }
}
