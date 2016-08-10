using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BC.EQCS.DataTransfer;
using BC.EQCS.Contracts;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class IncidentActivityLogRepository : Repository<IncidentActivityLog, IncidentActivityLogModel>, IAspectRepository<IncidentActivityLogModel, IncidentMasterModel>
    {
        public IncidentActivityLogRepository(IEntityFactory entityFactory) : base(entityFactory)
        {
            base.KeyValue = log => log.Id;
        }

        public IEnumerable<IncidentActivityLogModel> GetFor(IncidentMasterModel model)
        {
            var log
                = Context
                    .IncidentActivityLogs
                    .Include(entry => entry.ApplicationUser)
                    .Where(entry => entry.IncidentId == model.Id)
                    .ToList()
                    .Select(Mapper.Map<IncidentActivityLogModel>);

            return log;
        }

        public override int Create(IncidentActivityLogModel model)
        {
            var entity = SaveLogEntry(model);

            return KeyValue != null ? KeyValue(entity) : 0;
        }

        private IncidentActivityLog SaveLogEntry(IncidentActivityLogModel model)
        {
            var entity = Mapper.Map<IncidentActivityLog>(model);

            entity.ApplicationUser = Context.Users.First(user => user.ObjectGUID == model.User.ObjectGuid);

            Context.IncidentActivityLogs.Add(entity);

            Context.SaveChanges();

            return entity;
        }
    }
}