using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace BC.EQCS.Entities.Models.Mapping
{
    public class NotificationMessageMap : EntityTypeConfiguration<NotificationMessage>
    {
        public NotificationMessageMap()
        {
           HasKey(t => t.Id);

            Property(t => t.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
           
            ToTable("NotificationMessage");
            Property(t => t.Recipient).HasColumnName("Recipient");
            Property(t => t.Subject).HasColumnName("Subject");
            Property(t => t.Body).HasColumnName("Body");
            Property(t => t.Succeed).HasColumnName("Succeed");
            Property(t => t.Error).HasColumnName("Error");
        }
    }
}
