using System;
using System.ComponentModel.DataAnnotations;

namespace FlexmodulBackendV2.Domain
{
    public abstract class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
