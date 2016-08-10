using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using BC.EQCS.Security.Models;
using BC.Security.Internal.Contracts.Models;

namespace BC.Security.Internal.Contracts
{
    public interface ISecurityUserRepository 
    {
        Task<UserModel> GetUserByObjectGuid(Guid objectGuid);
        Task<UserModel> GetUserForClaimsPrincipal(ClaimsPrincipal claimsPrincipal);
        Task<SecurityUserModel> GetUserIdentity(IIdentity windowsIdentity);
    }
}