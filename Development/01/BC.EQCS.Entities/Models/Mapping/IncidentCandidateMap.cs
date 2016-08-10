using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class IncidentCandidateMap : EntityTypeConfiguration<IncidentCandidate>
    {
        public IncidentCandidateMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.RowVersion).IsRowVersion();

            ToTable("IncidentCandidates");

            HasOptional(t => t.Nationality)
                .WithMany()
                .HasForeignKey(d => d.NationalityId);
        }
    }
}