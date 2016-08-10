using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Entities.Utils;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class IncidentActionViewRepository : Repository<IncidentAction, IncidentActionViewModel>, IAspectRepository<IncidentActionViewModel, IncidentViewModel>
    {
        private readonly IUserContext _userContext;

        public IncidentActionViewRepository(IEntityFactory entityFactory, IUserContext userContext)
            : base(entityFactory)
        {
            _userContext = userContext;
            KeyValue = action => action.Id;
        }

        public IEnumerable<IncidentActionViewModel> GetFor(IncidentViewModel model)
        {
            var values = Context
                .IncidentActions
                .Where(actions => actions.IncidentId == model.Id)
                .Select(Mapper.Map<IncidentActionViewModel>);

            return values;
        }


        public override IncidentActionViewModel GetById(int id)
        {
            var incidentAction = Context.IncidentActions
                .IncludeAllNavigationProperties(Context)
                .Include(ia => ia.AssignedTo)
                .First(act => act.Id.Equals(id));

            var mappedAction = Mapper.Map<IncidentActionViewModel>(incidentAction);
            
            
            mappedAction.DocumentList = Context.Documents
                    .Where(p => p.OwnerType == "Action" && p.OwnerIdentifier.Value == id)
                    .ToList()
                    .Select(Mapper.Map<DocumentViewModel>)
                    .ToList();
            
            
            return mappedAction;
        }


        public override int Create(IncidentActionViewModel model)
        {
            var entity = Mapper.Map<IncidentAction>(model);

            var collection = GetUsersFromGuids(model.AssignedTo);
            entity.AssignedBy = Context.Users.FirstOrDefault(user => user.ObjectGUID.Equals(_userContext.CurrentUser.ObjectGuid));
            entity.AssignedOn = DateTime.UtcNow;
            entity.Status = IncidentActionStatus.InProgress;
            entity.AssignedTo = collection;
            Context.IncidentActions.Add(entity);
            Context.SaveChanges();

            return KeyValue != null ? KeyValue(entity) : 0;
        }

        public override void Update(IncidentActionViewModel model)
        {
            var entity = Context.IncidentActions.First(ia => ia.Id == model.Id);

            entity.ActionDescription = model.ActionDescription;
            entity.ActionResponse = model.ActionResponse;

            if (!String.IsNullOrEmpty(model.ActionResponse))
                entity.Status = IncidentActionStatus.Closed;

            Context.SaveChanges();
        }


        private ICollection<ApplicationUser> GetUsersFromGuids(ICollection<string> userGuidsIn)
        {
            var users = Context.Users.Where(au => userGuidsIn.Contains(au.ObjectGUID.ToString()));

            return users.ToArray();
        }
    }
}