using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.DataTransfer
{
    public static partial class Mapper
    {
        private static void MapReferences()
        {
            AutoMapper.Mapper.CreateMap<TestCentre, TestCentreModel>()
                .ForMember(dest => dest.Code,
                    opts => opts.MapFrom(src => src.CentreNumber))
                .ForMember(dest => dest.Name,
                    opts => opts.MapFrom(src => src.Name));

            AutoMapper.Mapper.CreateMap<TestLocation, TestLocationModel>()
                .ForMember(dest => dest.Code,
                    opts => opts.MapFrom(src => src.AdminUnit.Code))
                .ForMember(dest => dest.Name,
                    opts => opts.MapFrom(src => src.AdminUnit.Name))
                .ForMember(dest => dest.Country, opts => opts.Ignore());

            AutoMapper.Mapper.CreateMap<Organisation, OrganisationModel>()
                .ForMember(dest => dest.Code,
                    options => options.MapFrom(src => src.Id));

            AutoMapper.Mapper.CreateMap<Country, CountryModel>()
                .ForMember(dest => dest.Code,
                    opts => opts.MapFrom(src => src.IsoCode))
                .ForMember(dest => dest.Name,
                    opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsActive,
                    opts => opts.MapFrom(src => src.IsActive));
            
            AutoMapper.Mapper.CreateMap<Product, ProductModel>();
            
            AutoMapper.Mapper.CreateMap<Organisation, OrganisationModel>()
                .ForMember(dest => dest.Code,
                    opts => opts.MapFrom(src => src.Code));

            AutoMapper.Mapper.CreateMap<OrganisationType, OrganisationTypeModel>()
                .ForMember(model => model.Organisations,
                    options => options.MapFrom(entity => entity.Organisations));

            AutoMapper.Mapper.CreateMap<RiskRating, RiskRatingModel>();

            AutoMapper.Mapper.CreateMap<ResidualRiskRating, ResidualRiskRatingModel>();

            AutoMapper.Mapper.CreateMap<UkviImmediateReportType, UkviImmediateReportTypeModel>();
        }
    }
}