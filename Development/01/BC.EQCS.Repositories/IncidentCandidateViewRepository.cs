using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class IncidentCandidateViewRepository : Repository<IncidentCandidateView, IncidentCandidateViewModel>,
        IAspectRepository<IncidentCandidateViewModel, IncidentMasterModel>
    {
        public IncidentCandidateViewRepository(IEntityFactory entityFactory)
            : base(entityFactory)
        {
        }

        public override IncidentCandidateViewModel GetById(int id)
        {
            var query = Context.IncidentCandidateViews;

            var entity = query.FirstOrDefault(view => view.Id == id);

            var model = Mapper.Map<IncidentCandidateViewModel>(entity);

            return model;
        }

        public IEnumerable<IncidentCandidateViewModel> GetFor(IncidentMasterModel masterModel)
        {
            var values = Context
                .IncidentCandidateViews
                .Where(view => view.IncidentId == masterModel.Id)
                .Select(Mapper.Map<IncidentCandidateViewModel>);

            return values;
        }
    }
}