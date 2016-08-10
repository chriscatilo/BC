using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class UserRoleToClassToSchemaKeyMap : EntityTypeConfiguration<UserRoleToClassToSchemaKey>
    {
        public UserRoleToClassToSchemaKeyMap()
        {
            HasKey(t => new { t.ApplicationRoleId, t.IncidentClassId } );

            ToTable("UserRoleToClassToSchemaKey");

            HasRequired(t => t.ApplicationRole)
                .WithMany(t => t.SchemaKeys)
                .HasForeignKey(d => d.ApplicationRoleId);

            HasRequired(t => t.IncidentClass)
                .WithMany()
                .HasForeignKey(d => d.IncidentClassId);
        }
    }
}
