using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace BC.Security.Internal.Contracts.Models
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class UserModel 
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public Guid ObjectGuid { get; set; }

        public RoleModel Role { get; set; }
        public bool Enabled { get; set; }
        public abstract ClaimsIdentity GetClaimsIdentity();
    }

    public class WindowsUserModel : UserModel
    {
        private readonly WindowsPrincipal _principal;

        public WindowsUserModel(WindowsPrincipal principal)
        {
            _principal = principal;
            WindowsAccountName = _principal.Identity.Name;
            var sidClaim = _principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid);
            PrimarySid = sidClaim != null ? sidClaim.Value : string.Empty;
        }

        public string WindowsAccountName { get; private set; }
        public string Department { get; set; }
        public string Country { get; set; }
        public string PrimarySid { get; private set; }

        public override ClaimsIdentity GetClaimsIdentity()
        {
            if (_principal == null)
            {
                return null;
            }

            return _principal.Identity as ClaimsIdentity;
        }
    }

  
}
