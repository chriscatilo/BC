using System;

namespace BC.EQCS.Repositories.Security
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found in Active Directory")
        {
            
        }
    }
}