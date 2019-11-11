using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulAPI.Models
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


        public int MaterialId { get; set; }
        public HouseSections HouseSection { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Supplier { get; set; }
        public string Units { get; set; }
        public float PricePerUnit { get; set; }
    }
}
