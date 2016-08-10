using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BC.EQCS.Security.Constants;
using Newtonsoft.Json;

namespace BC.EQCS.Security.Models
{
    public class SecurityUserModel : BC.Security.Internal.Contracts.Models.UserModel
    {
        private readonly ClaimsPrincipal _claimsPrincipal;

        public SecurityUserModel()
        {
            
        }

        public SecurityUserModel(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }

        public int Id { get; set; }

        public string WindowsAccountName { get; set; }

        public string DisplayName { get; set; }

        public ICollection<BC.EQCS.Security.Models.RoleModel> ApplicationRoles { get; set; }

        [JsonIgnore]
        public bool IsVerificationTeamMember
        {
            get
            {
                if (ApplicationRoles == null)
                {
                    return false;
                }

                return ApplicationRoles.Any(role => role.ShortCode == RoleType.VerficationTeamCode);
            }
        }

        [JsonIgnore]
        public bool IsTestCenterStaff
        {
            get
            {
                if (ApplicationRoles == null)
                {
                    return false;
                }


                return ApplicationRoles.Any(role => role.ShortCode == RoleType.TestCenterStaff || role.ShortCode == RoleType.ExternalTestCenterStaff);
            }
        }

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
