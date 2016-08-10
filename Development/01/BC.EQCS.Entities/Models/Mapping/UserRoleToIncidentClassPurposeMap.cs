using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class UserRoleToIncidentClassPurposeMap : EntityTypeConfiguration<UserRoleToIncidentClassPurpose>
    {
        public UserRoleToIncidentClassPurposeMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Code)
                .HasMaxLength(50);

            Property(t => t.Description)
                .HasMaxLength(255);

            ToTable("UserRoleToIncidentClassPurpose");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.Description).HasColumnName("Description");
        }
    }
}