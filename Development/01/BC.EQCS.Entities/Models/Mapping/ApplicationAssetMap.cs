using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class ApplicationAssetMap : EntityTypeConfiguration<ApplicationAsset>
    {
        public ApplicationAssetMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Name)
                .HasMaxLength(255);

            Property(t => t.Code)
                .HasMaxLength(25);

            Property(t => t.Description)
                .HasMaxLength(255);

            ToTable("ApplicationAsset");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.Description).HasColumnName("Description");

            HasMany(t => t.ApplicationRoles)
                .WithMany(t => t.ApplicationAssets)
                .Map(m =>
                {
                    m.ToTable("ApplicationPermission");
                    m.MapLeftKey("ApplicationAssetId");
                    m.MapRightKey("ApplicationRoleId");
                });
        }
    }
}