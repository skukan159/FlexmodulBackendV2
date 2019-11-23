using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulBackendV2.Domain
{
    public class UserRoles
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
