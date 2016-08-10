using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.DataTransfer
{
    public static partial class Mapper
    {
        private static void MapNotifications()
        {
            //check this is ok ? 
            //notification message

            AutoMapper.Mapper
                .CreateMap<NotificationMapping, NotificationModel>()
                .ForMember(model => model.Role,
                    options => options.MapFrom(entity => entity.Role.Id))
                .ForMember(model => model.RaisedByRole,
                    options => options.MapFrom(entity => entity.RaisedByRole.Id));
        }

        private static void MapNotificationEvent()
        {
            //notification message
            //AutoMapper.Mapper
            //    .CreateMap<NotificationMessage, NotificationMessageModel>()
            //    .ForMember(model => model.)
        }

        private static void MapNotificationMessage()
        {
            //notification events
            //notification
            AutoMapper.Mapper
                .CreateMap<NotificationMessage, NotificationMessageModel>();

            AutoMapper.Mapper
                .CreateMap<NotificationMessageTemplate, NotificationMessageTemplateModel>()
                .ForMember(model => model.NotificationEvents,
                options => options.MapFrom(entity => entity.NotificationEvents.Id));
            

        }
    }
}
