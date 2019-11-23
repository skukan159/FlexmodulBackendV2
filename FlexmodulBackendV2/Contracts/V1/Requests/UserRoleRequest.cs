using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulBackendV2.Contracts.V1.Requests
{
    public class UserRoleRequest
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
