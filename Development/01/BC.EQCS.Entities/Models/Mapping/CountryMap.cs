using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Id)
             .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.IsoCode)
                .IsRequired()
                .HasMaxLength(2);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            ToTable("Country");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IsoCode).HasColumnName("IsoCode");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
