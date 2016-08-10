using System.Linq;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.DataTransfer
{
    public static partial class Mapper
    {
        private static void MapIncidentAction()
        {
            AutoMapper.Mapper
                .CreateMap<IncidentAction, IncidentActionModel>()
                .ForMember(model => model.AssignedTo, options => options.MapFrom(ent => ent.AssignedTo.Select(user => user.ObjectGUID)));

            AutoMapper.Mapper.CreateMap<IncidentActionModel, IncidentAction>()
                .ForMember(model => model.AssignedTo, options => options.Ignore());
        }

        private static void MapIncidentActionView()
        {
           AutoMapper.Mapper
                .CreateMap<IncidentAction, IncidentActionViewModel>()
                .ForMember(model => model.AssignedTo, options => options.MapFrom(ent => ent.AssignedTo.Select(user => user.DisplayName)));

           AutoMapper.Mapper.CreateMap<IncidentActionViewModel, IncidentAction>()
                .ForMember(model => model.AssignedTo, options => options.Ignore());
        }
    }
}