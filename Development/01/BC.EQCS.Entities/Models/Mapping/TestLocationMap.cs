using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class TestLocationMap : EntityTypeConfiguration<TestLocation>
    {
        public TestLocationMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            ToTable("TestLocation");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.CountryId).HasColumnName("CountryId");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.IsActive).HasColumnName("IsActive");

            HasRequired(t => t.Country)
                .WithMany(t => t.TestLocations)
                .HasForeignKey(t => t.CountryId);

            HasRequired(t => t.AdminUnit)
                .WithMany()
                .HasForeignKey(t => t.AdminUnitId);

        }
    }
}
