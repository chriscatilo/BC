using System;

namespace BC.EQCS.Repositories.Security
{
    public class ApplicationUserNotFoundException : Exception
    {
        public ApplicationUserNotFoundException() : base("Applciation User not found in database")
        {
            
        }
    }
}
