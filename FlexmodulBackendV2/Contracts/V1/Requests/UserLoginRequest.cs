using System.ComponentModel.DataAnnotations;

namespace FlexmodulBackendV2.Contracts.V1.Requests
{
    public class UserLoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
