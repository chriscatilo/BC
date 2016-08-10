using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class NotificationMappingMap : EntityTypeConfiguration<NotificationMapping>
    {
        public NotificationMappingMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
           
            ToTable("NotificationMapping");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.RoleId).HasColumnName("RoleId");
            Property(t => t.MessageTemplateId).HasColumnName("MessageTemplateId");
            Property(t => t.RaisedByRoleId).HasColumnName("RaisedByRoleId");
            
            HasOptional(t => t.Role)
               .WithMany(t => t.RoleNotifications)
               .HasForeignKey(d => d.RoleId);

            HasOptional(t => t.RaisedByRole)
               .WithMany(t => t.RaisedByRoleNotifications)
               .HasForeignKey(d => d.RaisedByRoleId);

            HasOptional(t => t.NotificationMessageTemplate)
               .WithMany(t => t.Notifications)
               .HasForeignKey(d => d.MessageTemplateId);
        }
    }
}
