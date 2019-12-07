﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FlexmodulBackendV2.Domain
{
    public class FmHouse
    {   
        [Key]
        public Guid Id { get; set; }
        public Guid HouseTypeId { get; set; }  
        public FmHouseType HouseType { get; set; }
        public int SquareMeters { get; set; }
    }
}
