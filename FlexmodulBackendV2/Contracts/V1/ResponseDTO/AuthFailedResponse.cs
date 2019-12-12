using System.Collections.Generic;

namespace FlexmodulBackendV2.Contracts.V1.ResponseDTO
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
