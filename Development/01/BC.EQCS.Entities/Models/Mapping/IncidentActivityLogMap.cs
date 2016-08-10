using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class IncidentActivityLogMap : EntityTypeConfiguration<IncidentActivityLog>
    {
        public IncidentActivityLogMap()
        {
            HasKey(t => t.Id);

            ToTable("IncidentActivityLog");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IncidentId).HasColumnName("IncidentId");
            Property(t => t.DateTimeOfActivity).HasColumnName("DateTimeOfActivity");
            Property(t => t.ApplicationUserId).HasColumnName("ApplicationUserId");
            Property(t => t.LogType).HasColumnName("LogType");
            Property(t => t.Payload).HasColumnName("Payload");

            HasRequired(t => t.ApplicationUser)
                .WithMany(t => t.IncidentActivityLogs)
                .HasForeignKey(d => d.ApplicationUserId);
            HasRequired(t => t.Incident)
                .WithMany(t => t.IncidentActivityLogs)
                .HasForeignKey(d => d.IncidentId);
        }
    }
}