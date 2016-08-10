using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Security.Models;

namespace BC.EQCS.Repositories.Security
{
    public class RolesRepository : Repository<ApplicationRole, RoleModel>
    {
        public RolesRepository(IEntityFactory entityFactory)
            : base(entityFactory)
        {
            base.KeyValue = Role => Role.Id;

        }

        public new IEnumerable<RoleModel> GetAll()
        {
            return Context.Set<ApplicationRole>().Include(x => x.ApplicationAssets).ToList().Select(Mapper.Map<RoleModel>);
        }        
    }

}
