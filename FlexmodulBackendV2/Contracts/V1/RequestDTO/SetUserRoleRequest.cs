using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulBackendV2.Contracts.V1.RequestDTO
{
    public class SetUserRoleRequest
    {
        public string UserId { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
