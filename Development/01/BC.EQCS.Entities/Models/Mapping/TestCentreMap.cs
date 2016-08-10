using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class TestCentreMap : EntityTypeConfiguration<TestCentre>
    {
        public TestCentreMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.CentreNumber)
                .HasMaxLength(5);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(40);

            ToTable("TestCentre");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.CentreNumber).HasColumnName("CentreNumber");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.IsActive).HasColumnName("IsActive");
            Property(t => t.OrganisationId).HasColumnName("OrganisationId");
            Property(t => t.AdminUnitId).HasColumnName("AdminUnitId");

            HasRequired(t => t.Organisation)
                .WithMany()
                .HasForeignKey(d => d.OrganisationId);

            HasRequired(t => t.AdminUnit)
                .WithMany()
                .HasForeignKey(d => d.AdminUnitId);
        }
    }
}
