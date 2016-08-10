using System.Collections.Generic;
using System.Linq;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class OrganisationRepository : Repository<Organisation, OrganisationModel>
    {
        public OrganisationRepository(IEntityFactory entityFactory)
            : base(entityFactory)
        {
        }

        public override IEnumerable<OrganisationModel> GetAll()
        {
            return Context.Set<Organisation>()
                .Include("OrganisationType")
                .Select(Mapper.Map<OrganisationModel>);
        }
    }
}
