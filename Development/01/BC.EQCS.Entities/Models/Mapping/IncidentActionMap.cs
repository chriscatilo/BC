using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class IncidentActionMap : EntityTypeConfiguration<IncidentAction>
    {
        public IncidentActionMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.ActionDescription)
                .IsRequired();

            ToTable("IncidentActions");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IncidentId).HasColumnName("IncidentId");
            Property(t => t.AssignedById).HasColumnName("AssignedById");
            Property(t => t.AssignedOn).HasColumnName("AssignedOn");
            Property(t => t.ActionDescription).HasColumnName("ActionDescription");
            Property(t => t.Status).HasColumnName("Status").HasColumnType("tinyint");
            Property(t => t.AssignedToTestCentre).HasColumnName("AssignedToTestCentre");
            
            //Property(t => t.Comments).HasColumnName("Comments");
            Property(t => t.RowVersion).IsRowVersion();

            HasRequired(t => t.Incident)
                .WithMany(t => t.IncidentActions)
                .HasForeignKey(d => d.IncidentId);

            HasRequired(t => t.AssignedBy)
                .WithMany(t => t.IncidentActionsAssigned)
                .HasForeignKey(t => t.AssignedById);

            HasMany<ApplicationUser>(s => s.AssignedTo)
                   .WithMany(c => c.IncidentActionsAssignedTo)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("IncidentActionId");
                       cs.MapRightKey("ApplicationUserId");
                       cs.ToTable("ActionToAssigneeUser");
                   });
        }
    }
}