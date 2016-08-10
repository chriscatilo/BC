using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class ApplicationUserMap : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMap()
        {
            HasKey(t => t.Id);

            Property(t => t.DisplayName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(100);

            Property(t => t.Department)
                .HasMaxLength(128);

            Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(64);

            Property(t => t.Surname)
                .IsRequired()
                .HasMaxLength(64);

            Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(128);

            Property(t => t.Login)
                .IsRequired()
                .HasMaxLength(128);

            Property(t => t.JobTitle)
                .HasMaxLength(128);

            Property(t => t.Telephone)
                .HasMaxLength(128);

            Property(t => t.Country)
                .HasMaxLength(128);

            ToTable("ApplicationUser");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.ObjectGUID).HasColumnName("ObjectGUID");
            Property(t => t.DisplayName).HasColumnName("DisplayName");
            Property(t => t.Department).HasColumnName("Department");
            Property(t => t.FirstName).HasColumnName("FirstName");
            Property(t => t.Surname).HasColumnName("Surname");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.Login).HasColumnName("Login");
            Property(t => t.JobTitle).HasColumnName("JobTitle");
            Property(t => t.Telephone).HasColumnName("Telephone");
            Property(t => t.Country).HasColumnName("Country");
            Property(t => t.DefaultCountryId).HasColumnName("DefaultCountryId");
            Property(t => t.Enabled).HasColumnName("Enabled");
            Property(t => t.ApplicationCountryDepartmentId).HasColumnName("ApplicationCountryDepartmentId");
            Property(t => t.SelectedCountryId).HasColumnName("SelectedCountryId");

            Property(t => t.UserName).HasColumnName("ExternalUserName");
            Property(t => t.PasswordHash).HasColumnName("ExternalUserPasswordHash");
            Property(t => t.SecurityStamp).HasColumnName("ExternalUserSecurityStamp");

            
            HasOptional(t => t.tblCountry)
                .WithMany(t => t.ApplicationUsers)
                .HasForeignKey(d => d.DefaultCountryId);

            HasMany<IncidentAction>(s => s.IncidentActionsAssignedTo)
                   .WithMany(c => c.AssignedTo)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("ApplicationUserId");
                       cs.MapRightKey("IncidentActionId");
                       cs.ToTable("ActionToAssigneeUser");
                   });

            HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);
            
            Ignore(t => t.EmailConfirmed);
            Ignore(t => t.PhoneNumber);
            Ignore(t => t.PhoneNumberConfirmed);
            Ignore(t => t.TwoFactorEnabled);
            Ignore(t => t.LockoutEndDateUtc);
            Ignore(t => t.LockoutEnabled);
            Ignore(t => t.AccessFailedCount);
        }
    }
}