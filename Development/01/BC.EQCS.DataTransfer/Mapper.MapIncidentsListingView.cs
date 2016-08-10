using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.DataTransfer
{
    public static partial class Mapper
    {
        private static void MapIncidentsListingView()
        {
            AutoMapper.Mapper.CreateMap<IncidentsListingView, IncidentsListingModel>()
                .ForMember(model => model.IncidentNumber,
                    options => options.MapFrom(entity => entity.IncidentNumber))
                 .ForMember(model => model.DisplayedCatOrSubCat,
                    options => options.MapFrom(entity => entity.DisplayedCatOrSubCat))
                .ForMember(model => model.TestCentreNumber,
                    options => options.MapFrom(entity => entity.TestCentreNumber))
                .ForMember(model => model.LoggedBy,
                    options => options.MapFrom(entity => entity.LoggedBy.Trim()))
                //Product
                .ForMember(model => model.Product,
                    options => options.MapFrom(entity => entity.Product))
                .ForMember(model => model.Category,
                    options => options.MapFrom(entity => entity.Category))
                .ForMember(model => model.SubCategory,
                    options => options.MapFrom(entity => entity.SubCategory))
                //Test Date
                .ForMember(model => model.TestDate,
                    options => options.MapFrom(entity => entity.TestDate))
                //Incident Date
                .ForMember(model => model.IncidentDate,
                    options => options.MapFrom(entity => entity.IncidentDate))
                //Active Actions
                .ForMember(model => model.HasActiveAction,
                    options => options.MapFrom(entity => entity.HasActiveAction))
                //ReportUkvi
                .ForMember(model => model.ReportUkvi, options => options.MapFrom(entity => entity.ReportUkvi));


            AutoMapper.Mapper.CreateMap<IncidentsListingModel, IncidentsListingView>();
        }
    }
}