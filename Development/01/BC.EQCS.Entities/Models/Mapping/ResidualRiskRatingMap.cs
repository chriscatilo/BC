using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class ResidualRiskRatingMap : EntityTypeConfiguration<ResidualRiskRating>
    {
        public ResidualRiskRatingMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(4);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            ToTable("ResidualRiskRating");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}