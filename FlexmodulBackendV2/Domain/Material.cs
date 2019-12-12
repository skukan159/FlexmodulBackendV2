using System.Collections.Generic;

namespace FlexmodulBackendV2.Domain
{
    public class Material : EntityBase
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

        public HouseSections HouseSection { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Supplier { get; set; }
        public string Units { get; set; }
        public float PricePerUnit { get; set; }
        public List<MaterialOnHouseType> MaterialOnHouseTypes { get; set; }
    }
}
