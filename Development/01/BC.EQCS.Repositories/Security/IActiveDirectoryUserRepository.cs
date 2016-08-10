using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BC.EQCS.Security.Models;

namespace BC.EQCS.Repositories.Security
{
    public interface IActiveDirectoryUserRepository
    {
        Task<ActiveDirectoryUser> GetUserByObjectGuid(Guid objectGuid);
        Task<IEnumerable<ActiveDirectoryUser>> GetUsersBySearchFilter(SearchFilter filter);
    }
}