using BC.EQCS.Entities.Models;
using BC.EQCS.Entities.Utils;
using BC.EQCS.Models;

namespace BC.EQCS.DataTransfer
{
    public static partial class Mapper
    {
        private static void MapIncidentClass()
        {
            AutoMapper.Mapper
                .CreateMap<IncidentClass, IncidentClassModel>()
                .ForMember(model => model.Code, options => options.MapFrom(entity => entity.Code))
                .ForMember(model => model.Type,
                    options => options.MapFrom(entity => entity.Type.GetValueOf(type => type.Code)))
                .ForMember(model => model.Parent, options => options.Ignore())
                .ForMember(model => model.Children, options => options.Ignore());
        }
    }
}