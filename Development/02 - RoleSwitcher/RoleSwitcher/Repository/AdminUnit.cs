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
    
    public partial class AdminUnit
    {
        public AdminUnit()
        {
            this.AdminUnit1 = new HashSet<AdminUnit>();
            this.UserToRoleToAdminUnits = new HashSet<UserToRoleToAdminUnit>();
            this.TestCentres = new HashSet<TestCentre>();
            this.TestLocations = new HashSet<TestLocation>();
        }
    
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> ParentId { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public string UbiquitousCode { get; set; }
    
        public virtual ICollection<AdminUnit> AdminUnit1 { get; set; }
        public virtual AdminUnit AdminUnit2 { get; set; }
        public virtual AdminUnitType AdminUnitType { get; set; }
        public virtual ICollection<UserToRoleToAdminUnit> UserToRoleToAdminUnits { get; set; }
        public virtual ICollection<TestCentre> TestCentres { get; set; }
        public virtual ICollection<TestLocation> TestLocations { get; set; }
    }
}
