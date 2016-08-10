using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class OrganisationMap : EntityTypeConfiguration<Organisation>
    {
        public OrganisationMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);
            
            ToTable("Organisation");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.IsActive).HasColumnName("IsActive");
            Property(t => t.OrganisationTypeId).HasColumnName("OrganisationTypeId");

            HasOptional(t => t.OrganisationType)
               .WithMany(t => t.Organisations)
               .HasForeignKey(d => d.OrganisationTypeId);
        }
    }
}
