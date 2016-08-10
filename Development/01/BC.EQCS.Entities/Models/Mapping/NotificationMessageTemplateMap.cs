using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class NotificationMessageTemplateMap : EntityTypeConfiguration<NotificationMessageTemplate>
    {
        public NotificationMessageTemplateMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
           
            ToTable("NotificationMessageTemplate");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.BodyText).HasColumnName("BodyText");
            Property(t => t.SubjectLine).HasColumnName("SubjectLine");
            Property(t => t.EventId).HasColumnName("EventId");
            Property(t => t.AssignedToTestCentre).HasColumnName("AssignedToTestCentre");

            HasOptional(t => t.NotificationEvents)
              .WithMany(t => t.NotificationMessageTemplate)
              .HasForeignKey(d => d.EventId);
          
        }
    }
}
