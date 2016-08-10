using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class AdminUnitMap : EntityTypeConfiguration<AdminUnit>
    {
        public AdminUnitMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Code)
                .HasMaxLength(255);

            Property(t => t.Name)
                .HasMaxLength(255);

            Property(t => t.Description)
                .HasMaxLength(255);

            ToTable("AdminUnit");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.TypeId).HasColumnName("TypeId");
            Property(t => t.ParentId).HasColumnName("ParentId");

            HasOptional(t => t.Parent)
                .WithMany(t => t.Children)
                .HasForeignKey(d => d.ParentId);

            HasRequired(t => t.Type)
                .WithMany()
                .HasForeignKey(d => d.TypeId);
        }
    }
}
