using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.DataTransfer
{
    public static partial class Mapper
    {
        private static void MapIncidentActivityLog()
        {
            AutoMapper.Mapper
                .CreateMap<IncidentActivityLog, IncidentActivityLogModel>()
                .ForMember(model => model.User,
                    options => options.MapFrom(entity => Map<UserModel>(entity.ApplicationUser)))
                .ReverseMap();

            AutoMapper.Mapper
                .CreateMap<IncidentGenericWorkflowModel, IncidentActivityLogModel>()
                .ForMember(entry => entry.Payload, options => options.MapFrom(source => source.LogType.ToString()));

            AutoMapper.Mapper
                .CreateMap<IncidentRejectionModel, IncidentActivityLogModel>()
                .ForMember(logEntry => logEntry.Payload, options => options.MapFrom(source => source.Reason));

            AutoMapper.Mapper
                .CreateMap<IncidentReopeningModel, IncidentActivityLogModel>()
                .ForMember(logEntry => logEntry.Payload, options => options.MapFrom(source => source.Reason));

            AutoMapper.Mapper
                .CreateMap<IncidentClosureModel, IncidentActivityLogModel>()
                .ForMember(logEntry => logEntry.Payload, options => options.MapFrom(source => source.Comments));
        }
    }
}