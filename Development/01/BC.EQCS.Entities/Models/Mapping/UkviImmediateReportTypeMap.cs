using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class UkviImmediateReportTypeMap : EntityTypeConfiguration<UkviImmediateReportType>
    {
        public UkviImmediateReportTypeMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("UkviImmediateReportType");
        }
    }
}