using System;
using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Entities.Utils;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class IncidentCandidateRepository : Repository<IncidentCandidate, IncidentCandidateModel>, IAspectRepository<IncidentCandidateModel, IncidentMasterModel>
    {
        public IncidentCandidateRepository(IEntityFactory entityFactory) : base(entityFactory)
        {
            KeyValue = candidate => candidate.Id;
        }

        public override int Create(IncidentCandidateModel model)
        {
            var entity = new IncidentCandidate
            {
                IncidentId = model.IncidentId
            };

            Context.IncidentCandidates.Add(entity);
           
            SaveCandidate(model, entity);

            return KeyValue != null ? KeyValue(entity) : 0;
        }

        public override IncidentCandidateModel GetById(int id)
        {
            var entity = Context.IncidentCandidates.IncludeAllNavigationProperties(Context)
                .FirstOrDefault(item => item.Id == id);

            var model = Mapper.Map<IncidentCandidateModel>(entity);

            return model;
        }

        private void SaveCandidate(IncidentCandidateModel model, IncidentCandidate entity)
        {
            Mapper.Map(model, entity);

            Context.Countries
                .GetByIdentifier(model.Nationality,
                    (values, code) =>
                        values.FirstOrDefault(
                            value => value.IsoCode.Equals(code, StringComparison.CurrentCultureIgnoreCase)))
                .SetValue(entity, candidate => candidate.Nationality);

            Context.SaveChanges();
        }

        public override void Delete(int id)
        {
            var candidate = Context.IncidentCandidates.FirstOrDefault(i => i.Id == id);

            if (candidate == null) return;

            Context.IncidentCandidates.Remove(candidate);

            Context.SaveChanges();
        }

        public IEnumerable<IncidentCandidateModel> GetFor(IncidentMasterModel masterModel)
        {
            var entity = Context
                .Incidents
                .FirstOrDefault(incident => incident.Id == masterModel.Id);
            if (entity == null)
            {
                return new IncidentCandidateModel[0];
            }

            var values = entity.IncidentCandidates
                .Select(Mapper.Map<IncidentCandidateModel>);

            return values;
        }

        public override void Update(IncidentCandidateModel value)
        {
            var entity =
                Context.IncidentCandidates.FirstOrDefault(
                    item => item.Id == value.Id && item.IncidentId == value.IncidentId);

            SaveCandidate(value, entity);
        }
    }
}