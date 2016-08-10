using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class AdminUnitTypeMap : EntityTypeConfiguration<AdminUnitType>
    {
        public AdminUnitTypeMap()
        {
            HasKey(t => t.Id);

            // TODO CHris: truncate
            Property(t => t.Code)
                .HasMaxLength(255);

            Property(t => t.Name)
                .HasMaxLength(255);

            Property(t => t.Description)
                .HasMaxLength(255);

            ToTable("AdminUnitType");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Description).HasColumnName("Description");
        }
    }
}