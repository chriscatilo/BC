using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            HasKey(t => t.ProductId);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(4);

            ToTable("Product");
            Property(t => t.ProductId).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.IsActive).HasColumnName("IsActive");
            Property(t => t.Code).HasColumnName("Code");
        }
    }
}