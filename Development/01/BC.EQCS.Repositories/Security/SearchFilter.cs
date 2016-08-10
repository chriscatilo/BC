using System;

namespace BC.EQCS.Repositories.Security
{
    public class SearchFilter
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public Guid? ObjectGuid { get; set; }
    }
}