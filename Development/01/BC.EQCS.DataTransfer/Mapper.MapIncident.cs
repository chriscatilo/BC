using BC.EQCS.Entities.Models;
using BC.EQCS.Entities.Utils;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.DataTransfer
{
    public static partial class Mapper
    {
        private static void MapIncident()
        {
            AutoMapper.Mapper
                .CreateMap<Incident, IncidentModel>()
                .ForMember(model => model.RiskRating,
                    options => options.MapFrom(entity => entity.RiskRating.GetValueOf(item => item.Code)))
                .ForMember(model => model.ResidualRiskRating,
                    options => options.MapFrom(entity => entity.ResidualRiskRating.GetValueOf(item => item.Code)))
                .ForMember(model => model.Product,
                    options => options.MapFrom(entity => entity.Product.GetValueOf(item => item.Code)))
                .ForMember(model => model.Category,
                    options =>
                        options.MapFrom(
                            entity =>
                                entity.IncidentClass.GetAscendantIncidentClassCode(Constants.IncidentClassTypes.Category)))
                .ForMember(model => model.SubCategory,
                    options =>
                        options.MapFrom(
                            entity =>
                                entity.IncidentClass.GetAscendantIncidentClassCode(
                                    Constants.IncidentClassTypes.SubCategory)))
                .ForMember(model => model.ReferringOrgType,
                    options => options.MapFrom(entity => entity.ReferringOrgType.GetValueOf(item => item.Code)))
                .ForMember(model => model.ReferringOrganisation,
                    options => options.MapFrom(entity => entity.GetReferringOrganisation()))
                .ForMember(model => model.ReferringOrgExists,
                    options => options.MapFrom(entity => entity.GetReferringOrgExists()))
                .ForMember(model => model.ReferringOrgCountry,
                    options => options.MapFrom(entity => entity.ReferringOrgCountry.GetValueOf(item => item.IsoCode)))
                .ForMember(model => model.TestLocation,
                    options =>
                        options.MapFrom(
                            entity =>
                                entity.TestLocation.GetAscendantAdminUnitCode(Constants.AdminUnitTypes.TestLocation)))
                .ForMember(model => model.TestCentre,
                    options => options.MapFrom(entity => entity.TestCentre.CentreNumber))

                .ForMember(model => model.NoOfCandidates,
                    options => options.MapFrom(entity => entity.NumberOfCandidatesAffected))

                .ForMember(model => model.UkviImmediateReportType,
                    options => options.MapFrom(entity => entity.UkviImmediateReportType.GetValueOf(item => item.Code)));

            AutoMapper.Mapper
                .CreateMap<IncidentModel, Incident>()
                .ForMember(entity => entity.Status, options => options.Ignore())
                .ForMember(entity => entity.NumberOfCandidatesAffected,
                    options => options.MapFrom(entity => entity.NoOfCandidates))
                .ForMember(entity => entity.IncidentActions,
                    opt => opt.Ignore())
                .ForMember(entity => entity.ReferringOrgName,
                    opt => opt.MapFrom(model => (model.ReferringOrgExists == true ? null : model.ReferringOrganisation)))
                .IgnoreEntityRelations();

            AutoMapper.Mapper
                .CreateMap<IncidentClosureModel, IncidentModel>();
                 
            AutoMapper.Mapper
                .CreateMap<IncidentView, IncidentViewModel>()
                .ForMember(model => model.NoOfCandidates,
                    options => options.MapFrom(entity => entity.NumberOfCandidatesAffected));
              
            AutoMapper.Mapper
                .CreateMap<Incident, IncidentMasterModel>()
                .ForMember(model => model.IncidentClass,
                    options => options.MapFrom(entity => entity.IncidentClass.GetValueOf(item => item.Code)))
            .ForMember(model => model.TestCentre,
                    options => options.MapFrom(entity => entity.TestCentre.AdminUnit.Code))
            .ForMember(model => model.TestLocation,
                    options => options.MapFrom(entity => entity.TestLocation.AdminUnit.Code));


            AutoMapper.Mapper
                .CreateMap<IncidentViewModel, UkviImmediateReportModel>();

        }
    }
}