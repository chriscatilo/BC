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
    
    public partial class AdminUnitType
    {
        public AdminUnitType()
        {
            this.AdminUnits = new HashSet<AdminUnit>();
            this.ApplicationRoles = new HashSet<ApplicationRole>();
        }
    
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<AdminUnit> AdminUnits { get; set; }
        public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }
    }
}
