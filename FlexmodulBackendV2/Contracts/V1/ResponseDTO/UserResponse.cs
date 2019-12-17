using System.Collections.Generic;

namespace FlexmodulBackendV2.Contracts.V1.ResponseDTO
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
