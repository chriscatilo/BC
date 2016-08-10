using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BC.EQCS.Entities.Models
{
    public class ApplicationUser : 
        IdentityUser<int, EqcsEntities.CustomIdentityUserLogin, EqcsEntities.CustomIdentityUserRole, EqcsEntities.CustomIdentityUserClaim>
    {
        public Guid ObjectGUID { get; set; }
        public string DisplayName { get; set; }
        public string Department { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public string Login { get; set; }
        public string JobTitle { get; set; }
        public string Telephone { get; set; }
        public string Country { get; set; }
        public int? DefaultCountryId { get; set; }
        public bool Enabled { get; set; }
        public int? ApplicationCountryDepartmentId { get; set; }
        public int? SelectedCountryId { get; set; }
        public virtual Country tblCountry { get; set; }

        public virtual ICollection<UserToRoleToAdminUnit> UserToRoleToAdminUnits { get; set; }
        public virtual ICollection<IncidentActivityLog> IncidentActivityLogs { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
        public virtual ICollection<IncidentAction> IncidentActionsAssigned { get; set; }
        public virtual ICollection<IncidentAction> IncidentActionsAssignedTo { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            var identity = new ClaimsIdentity(
                 DefaultAuthenticationTypes.ApplicationCookie,
                 ClaimsIdentity.DefaultNameClaimType,
                 ClaimsIdentity.DefaultRoleClaimType);
          
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, this.Id.ToString(), ClaimTypes.NameIdentifier, DefaultAuthenticationTypes.ApplicationCookie));
            identity.AddClaim(new Claim(ClaimTypes.Name, this.Login, ClaimTypes.Name, DefaultAuthenticationTypes.ApplicationCookie));
            identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, this.Login, ClaimTypes.WindowsUserClaim, DefaultAuthenticationTypes.ApplicationCookie));
            identity.AddClaim(new Claim(Constants.DefaultSecurityStampClaimType, this.SecurityStamp));
            identity.AddClaim(new Claim(ClaimTypes.Role, Guid.NewGuid().ToString()));

            return identity;
        }
    }
}
