﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EQCSEntities : DbContext
    {
        public EQCSEntities()
            : base("name=EQCSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdminUnit> AdminUnits { get; set; }
        public virtual DbSet<AdminUnitType> AdminUnitTypes { get; set; }
        public virtual DbSet<ApplicationAsset> ApplicationAssets { get; set; }
        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<UserToRoleToAdminUnit> UserToRoleToAdminUnits { get; set; }
        public virtual DbSet<TestCentre> TestCentres { get; set; }
        public virtual DbSet<TestLocation> TestLocations { get; set; }
    }
}
