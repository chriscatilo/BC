using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    class IncidentActionListingMap : EntityTypeConfiguration<IncidentActionListing>
    {
        public IncidentActionListingMap()
        {
            HasKey(t => t.Id);
            
            ToTable("IncidentActionListingView");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IncidentId).HasColumnName("IncidentId");
            Property(t => t.AssignedTo).HasColumnName("AssignedTo");
            Property(t => t.AssignedBy).HasColumnName("AssignedBy");
            Property(t => t.AssignedOn).HasColumnName("AssignedOn");
            Property(t => t.ActionDescription).HasColumnName("ActionDescription");
            Property(t => t.Comments).HasColumnName("Comments");
            Property(t => t.IncidentId).HasColumnName("IncidentId");
            Property(t => t.AssignedById).HasColumnName("AssignedById");
            Property(t => t.Status).HasColumnName("Status");
            Property(t => t.StatusId).HasColumnName("StatusId");
        }
    }
}