using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class NotificationEventMap : EntityTypeConfiguration<NotificationEvent>
    {
        public NotificationEventMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
           
            ToTable("NotificationEvent");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.EventName).HasColumnName("EventName");
            Property(t => t.IsActive).HasColumnName("IsActive");

            
        }
    }
}
