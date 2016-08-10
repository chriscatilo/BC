using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class IncidentCandidateViewMap : EntityTypeConfiguration<IncidentCandidateView>
    {
        public IncidentCandidateViewMap()
        {
            HasKey(t => t.Id);
            ToTable("IncidentCandidateView");
        }
    }
}
