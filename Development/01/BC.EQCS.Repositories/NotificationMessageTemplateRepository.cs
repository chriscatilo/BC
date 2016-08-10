using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class NotificationMessageTemplateRepository : Repository<NotificationMessageTemplate, NotificationMessageTemplateModel>, INotificationTemplateRepository<NotificationMessageTemplateModel>
    {
        public NotificationMessageTemplateRepository(IEntityFactory entityFactory)
            : base(entityFactory)
        {
        }

        public override NotificationMessageTemplateModel GetById(int id)
        {
            var entity = Context
                .NotificationMessageTemplates
                .FirstOrDefault(template => template.Id == id);

            if (entity == null)
            {
                return null;
            }

            var model = Mapper.Map<NotificationMessageTemplateModel>(entity);
            return model;
        }

        public NotificationMessageTemplateModel GetTemplateBasedOnEventId(int eventId)
        {
            var entity = Context
                .NotificationMessageTemplates
                .FirstOrDefault(template => template.EventId == eventId);
            if (entity == null)
            {
                return null;
            }
            var model = Mapper.Map<NotificationMessageTemplateModel>(entity);
            return model;
        }

        public NotificationMessageTemplateModel GetTemplateBasedOnActionIdEventId(int eventId, int actionId)
        {
            //get the action 
            var assignTestCentre =
                Context.IncidentActions.FirstOrDefault(action => action.Id == actionId).AssignedToTestCentre;

            var entity = new object();
            if (assignTestCentre)
            {
                entity = Context.NotificationMessageTemplates
                .FirstOrDefault(template => template.EventId == eventId && template.AssignedToTestCentre == true);
            }
            else
            {
                entity = Context
                    .NotificationMessageTemplates
                    .FirstOrDefault(template => template.EventId == eventId && template.AssignedToTestCentre == false);
            }

            if (entity == null)
            {
                return null;
            }
            var model = Mapper.Map<NotificationMessageTemplateModel>(entity);
            return model;
        }

    }
}
