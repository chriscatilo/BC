using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class UserToRoleToAdminUnitMap : EntityTypeConfiguration<UserToRoleToAdminUnit>
    {
        public UserToRoleToAdminUnitMap()
        {
            HasKey(t => new { t.ApplicationUserId, t.ApplicationRoleId, t.AdminUnitId });

            Property(t => t.ApplicationUserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.ApplicationRoleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.AdminUnitId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            ToTable("UserToRoleToAdminUnit");
            Property(t => t.ApplicationUserId).HasColumnName("ApplicationUserId");
            Property(t => t.ApplicationRoleId).HasColumnName("ApplicationRoleId");
            Property(t => t.AdminUnitId).HasColumnName("AdminUnitId");

            HasRequired(t => t.AdminUnit)
                .WithMany(t => t.UserToRoleToAdminUnit)
                .HasForeignKey(d => d.AdminUnitId);
            HasRequired(t => t.ApplicationRole)
                .WithMany(t => t.UserToRoleToAdminUnits)
                .HasForeignKey(d => d.ApplicationRoleId);
            HasRequired(t => t.ApplicationUser)
                .WithMany(t => t.UserToRoleToAdminUnits)
                .HasForeignKey(d => d.ApplicationUserId);

        }
    }
}
