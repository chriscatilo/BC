using System;

namespace BC.EQCS.Repositories.Security
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException() : base("Application user already exists")
        {
            
        }
    }
}