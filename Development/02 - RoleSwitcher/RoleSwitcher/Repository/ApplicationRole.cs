//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoleSwitcher.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class ApplicationRole
    {
        public ApplicationRole()
        {
            this.ApplicationAssets = new HashSet<ApplicationAsset>();
            this.UserToRoleToAdminUnits = new HashSet<UserToRoleToAdminUnit>();
            this.AdminUnitTypes = new HashSet<AdminUnitType>();
        }
    
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool DataAuthorisation { get; set; }
    
        public virtual ICollection<ApplicationAsset> ApplicationAssets { get; set; }
        public virtual ICollection<UserToRoleToAdminUnit> UserToRoleToAdminUnits { get; set; }
        public virtual ICollection<AdminUnitType> AdminUnitTypes { get; set; }
    }
}
