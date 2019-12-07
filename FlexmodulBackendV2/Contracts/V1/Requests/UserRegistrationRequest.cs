namespace FlexmodulBackendV2.Contracts.V1.Requests
{
    public class UserRegistrationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SecretPassword { get; set; }
    }
}
