using System.Linq;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class IncidentViewRepository : Repository<IncidentView, IncidentViewModel>
    {
        public IncidentViewRepository(IEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override IncidentViewModel GetById(int id)
        {
            var query = Context.IncidentViews.AsNoTracking();

            var entity = query.FirstOrDefault(view => view.Id == id);

            var model = Mapper.Map<IncidentViewModel>(entity);

            return model;
        }
    }
}