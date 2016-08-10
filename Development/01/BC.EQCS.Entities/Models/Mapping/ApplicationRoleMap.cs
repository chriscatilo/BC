using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class ApplicationRoleMap : EntityTypeConfiguration<ApplicationRole>
    {
        public ApplicationRoleMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(t => t.Description)
                .HasMaxLength(255);

            Property(t => t.Code)
                .HasMaxLength(25);

            ToTable("ApplicationRole");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.DataAuthorisation).HasColumnName("DataAuthorisation");

            HasMany(t => t.IncidentClasses)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("Raisable_UserRoleToIncidentClass");
                    m.MapLeftKey("ApplicationRoleId");
                    m.MapRightKey("IncidentClassId");
                });

            HasMany(t => t.ViewableIncidentClasses)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("Viewable_UserRoleToIncidentClass");
                    m.MapLeftKey("ApplicationRoleId");
                    m.MapRightKey("IncidentClassId");
                });


            HasMany(t => t.ReadOnlyIncidentClasses)
               .WithMany()
               .Map(m =>
               {
                   m.ToTable("ReadOnly_UserRoleToIncidentClass");
                   m.MapLeftKey("ApplicationRoleId");
                   m.MapRightKey("IncidentClassId");
               });
        }
    }
}