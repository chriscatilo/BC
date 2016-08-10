using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class IncidentActivityListingViewMap : EntityTypeConfiguration<IncidentActivityListingView>
    {
        public IncidentActivityListingViewMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            ToTable("IncidentActivityListingView");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IncidentId).HasColumnName("IncidentId");
            Property(t => t.DateTimeOfActivity).HasColumnName("DateTimeOfActivity");
            Property(t => t.ApplicationUserId).HasColumnName("ApplicationUserId");
            Property(t => t.LogType).HasColumnName("LogType");
            Property(t => t.Payload).HasColumnName("Payload");
            Property(t => t.Username).HasColumnName("UserDisplayName");


        }
    }
}
