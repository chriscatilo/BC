using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class IncidentClassTypeMap : EntityTypeConfiguration<IncidentClassType>
    {
        public IncidentClassTypeMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Code)
                .HasMaxLength(7);

            Property(t => t.Name)
                .HasMaxLength(255);

            ToTable("IncidentClassType");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.Name).HasColumnName("Name");
        }
    }
}