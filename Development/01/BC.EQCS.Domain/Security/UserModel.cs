using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BC.EQCS.Domain.Security
{
    public class UserModel : BC.Security.Internal.Contracts.Models.UserModel
    {
        private readonly ClaimsPrincipal _claimsPrincipal;

        public UserModel()
        {
            
        }

        public UserModel(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }

        public string WindowsAccountName { get; set; }
        
        public  BC.EQCS.Domain.Security.RoleModel Role { get; set; }

        public BC.EQCS.Domain.Security.GeoArea GeoArea { get; set; }
        
        public override ClaimsIdentity GetClaimsIdentity()
        {
            if (_claimsPrincipal != null && _claimsPrincipal.Identity != null)
            {
               var claimsIdentity = _claimsPrincipal.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    return claimsIdentity;
                }
            }

            throw new InvalidOperationException("Unable to resolve Claims Identity for user");
        }
    }
}
