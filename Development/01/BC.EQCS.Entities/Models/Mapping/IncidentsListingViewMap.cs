using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class IncidentsListingViewMap : EntityTypeConfiguration<IncidentsListingView>
    {
        public IncidentsListingViewMap()
        {
            HasKey(t => new {t.Id, t.IncidentDate, t.Status, t.HasActiveAction});

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.IncidentNumber)
                .HasMaxLength(10);

            Property(t => t.TestCentreNumber)
                .HasMaxLength(255);

            Property(t => t.Product)
                .HasMaxLength(255);

            Property(t => t.Category)
                .HasMaxLength(255);
            
            Property(t => t.SubCategory)
                .HasMaxLength(255);

            Property(t => t.RaisedBy)
                .IsFixedLength()
                .HasMaxLength(100);

            Property(t => t.Status)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.HasActiveAction)
                .IsRequired()
                .HasMaxLength(5);

            ToTable("IncidentsListingView");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IncidentNumber).HasColumnName("Incident Number");
            Property(t => t.TestCentreNumber).HasColumnName("Test Centre Number");
            Property(t => t.Product).HasColumnName("Product");
            Property(t => t.DisplayedCatOrSubCat).HasColumnName("DisplayedCatOrSubCat");
            Property(t => t.Category).HasColumnName("Category");
            Property(t => t.CategoryCode).HasColumnName("CategoryCode");
            Property(t => t.SubCategory).HasColumnName("SubCategory");
            Property(t => t.SubCategoryCode).HasColumnName("SubCategoryCode");
            Property(t => t.RaisedBy).HasColumnName("Raised By");
            Property(t => t.LoggedBy).HasColumnName("Logged By");
            Property(t => t.IncidentDate).HasColumnName("Incident Date");
            Property(t => t.UkviFollowUpDate).HasColumnName("UkviFollowUpDate");
            Property(t => t.TestDate).HasColumnName("Test Date");
            Property(t => t.Status).HasColumnName("StatusName");
            Property(t => t.HasActiveAction).HasColumnName("HasActiveAction");
            Property(t => t.VenueAdminUnitCode).HasColumnName("VenueAdminUnitCode");
            Property(t => t.VenueAdminUnitId).HasColumnName("VenueAdminUnitId");
            Property(t => t.ReportUkvi).HasColumnName("ReportUkvi");
        }
    }
}