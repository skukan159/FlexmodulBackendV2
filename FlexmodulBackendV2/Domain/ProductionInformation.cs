﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlexmodulBackendV2.Domain
{
    public class ProductionInformation
    {
        [Key]
        public Guid Id { get; set; }
        public Guid HouseId { get; set; }
        public FmHouse House { get; set; }
        public List<Rent> Rents { get; set; }
        [Required]
        public Customer Customer { get; set; }
        public int? ExteriorWalls { get; set; }
        public int? Ventilation { get; set; }
        public string Note { get; set; }
        [Required]
        public int ProductionPrice { get; set; }
        [Required]
        public DateTime ProductionDate { get; set; }
        public ICollection<AdditionalCost> AdditionalCosts { get; set; }
        [Required]
        public User LastUpdatedBy { get; set; }
        [Required]
        public DateTime LastUpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}