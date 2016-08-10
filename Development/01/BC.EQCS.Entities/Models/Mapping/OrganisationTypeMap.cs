using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class OrganisationTypeMap : EntityTypeConfiguration<OrganisationType>
    {
        public OrganisationTypeMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(5);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            ToTable("OrganisationType");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.IsActive).HasColumnName("IsActive");
            
        }
    }
}