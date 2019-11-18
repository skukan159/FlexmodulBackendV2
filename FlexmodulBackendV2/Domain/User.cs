using System;
using System.ComponentModel.DataAnnotations;

namespace FlexmodulBackendV2.Domain
{
    public class User
    {
        public enum AuthenticationLevels
        {
            Guest,
            Employee,
            AdministrationEmployee,
            SuperUser
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }
        public AuthenticationLevels AuthenticationLevel { get; set; }
    }
}
