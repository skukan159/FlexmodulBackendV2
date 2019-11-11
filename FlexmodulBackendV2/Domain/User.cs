using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulAPI.Models
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

        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        public AuthenticationLevels AuthenticationLevel { get; set; }
    }
}
