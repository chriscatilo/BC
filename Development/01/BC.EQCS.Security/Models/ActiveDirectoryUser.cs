using System;

namespace BC.EQCS.Security.Models
{
    public class ActiveDirectoryUser
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string Department { get; set; }
        public string Country { get; set; }
        public Guid ObjectGuid { get; set; }
        public bool Defined { get; set; }
    }
}