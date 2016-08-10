using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class IncidentViewMap : EntityTypeConfiguration<IncidentView>
    {
        public IncidentViewMap()
        {
            HasKey(t => t.Id);
            ToTable("IncidentView");
        }
    }
}