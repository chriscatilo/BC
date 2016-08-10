using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoleSwitcher.Models
{
    public class UserRole
    {
        public User User { get; set; }
        public Role Role { get; set; }
        public AdminUnit AdminUnit { get; set; }
    }
}