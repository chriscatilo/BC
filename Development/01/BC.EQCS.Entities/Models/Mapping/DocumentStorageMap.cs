using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class DocumentStorageMap : EntityTypeConfiguration<DocumentStorage>
    {
        public DocumentStorageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ContentName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.ContentType)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.Content)
                .IsRequired();

            this.Property(t => t.OwnerType)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DocumentStorage");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ContentName).HasColumnName("ContentName");
            this.Property(t => t.ContentType).HasColumnName("ContentType");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.UploadedDate).HasColumnName("UploadedDate");
            this.Property(t => t.UploadedBy).HasColumnName("UploadedBy");
            this.Property(t => t.OwnerIdentifier).HasColumnName("OwnerIdentifier");
            this.Property(t => t.OwnerType).HasColumnName("OwnerType");
        }
    }
}
