using System.Data.Entity.ModelConfiguration;

namespace BC.EQCS.Entities.Models.Mapping
{
    public class IncidentMap : EntityTypeConfiguration<Incident>
    {
        public IncidentMap()
        {
            HasKey(t => t.Id);

            Property(t => t.FormalId)
                .HasMaxLength(10);

            Property(t => t.Description)
                .IsRequired();

            ToTable("Incident");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.FormalId).HasColumnName("FormalId");
            Property(t => t.Status).HasColumnName("Status");
            Property(t => t.CreateDate).HasColumnName("CreateDate");
            Property(t => t.RaisedDate).HasColumnName("RaisedDate");
            Property(t => t.IncidentDate).HasColumnName("IncidentDate");
            Property(t => t.IncidentTime).HasColumnName("IncidentTime");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.ProductId).HasColumnName("ProductId");
            Property(t => t.RaisedBy).HasColumnName("RaisedBy");
            Property(t => t.IncidentClassId).HasColumnName("IncidentClassId");
            Property(t => t.RiskRatingId).HasColumnName("RiskRatingId");
            Property(t => t.TestDate).HasColumnName("TestDate");
            Property(t => t.ResidualRiskRatingId).HasColumnName("ResidualRiskRatingId");
            Property(t => t.LoggedById).HasColumnName("LoggedById");
            Property(t => t.TestLocationId).HasColumnName("TestLocationId");
            Property(t => t.TestCentreId).HasColumnName("TestCentreId");
            Property(t => t.UkviFollowUpDate).HasColumnName("UkviFollowUpDate");
            
            Property(t => t.ReferringOrgName).HasColumnName("ReferringOrgName");
            Property(t => t.ReferringOrgSurname).HasColumnName("ReferringOrgSurname");
            Property(t => t.ReferringOrgFirstnames).HasColumnName("ReferringOrgFirstnames");
            Property(t => t.ReferringOrgJobTitle).HasColumnName("ReferringOrgJobTitle");
            Property(t => t.ReferringOrgEmail).HasColumnName("ReferringOrgEmail");

            Property(t => t.ReferringOrgCountryId).HasColumnName("ReferringOrgCountryId");
            Property(t => t.ReferringOrgTypeId).HasColumnName("ReferringOrgTypeId");
            Property(t => t.ReferringOrganisationId).HasColumnName("ReferringOrganisationId");

            Property(t => t.ReportUkvi).HasColumnName("ReportUKVI");

            Property(t => t.NumberOfCandidatesAffected).HasColumnName("NumberOfCandidatesAffected");

            Property(t => t.RowVersion).IsRowVersion();
            
            HasOptional(t => t.TestLocation)
                .WithMany()
                .HasForeignKey(d => d.TestLocationId);
            HasOptional(t => t.TestCentre)
                .WithMany()
                .HasForeignKey(d => d.TestCentreId);
            HasOptional(t => t.LoggedBy)
                .WithMany(t => t.Incidents)
                .HasForeignKey(d => d.LoggedById);
            HasOptional(t => t.Product)
                .WithMany(t => t.Incidents)
                .HasForeignKey(d => d.ProductId);
            HasOptional(t => t.ReferringOrgType)
                .WithMany(t => t.Incidents)
                .HasForeignKey(d => d.ReferringOrgTypeId);
            HasOptional(t => t.ReferringOrganisation)
                .WithMany(t => t.Incidents)
                .HasForeignKey(d => d.ReferringOrganisationId);
            HasOptional(t => t.ReferringOrgCountry)
                .WithMany(t => t.Incidents)
                .HasForeignKey(d => d.ReferringOrgCountryId);
         

            Property(t => t.IncidentClassId).HasColumnName("IncidentClassId");
            HasOptional(t => t.IncidentClass)
                .WithMany()
                .HasForeignKey(d => d.IncidentClassId);

            HasOptional(t => t.UkviImmediateReportType)
                .WithMany()
                .HasForeignKey(d => d.UkviImmediateReportTypeId);
        }
    }
}