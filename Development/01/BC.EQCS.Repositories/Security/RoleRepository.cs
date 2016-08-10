using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Threading.Tasks;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Security.Models;

namespace BC.EQCS.Repositories.Security
{
    public class RoleRepository : AsyncRepository<ApplicationRole, RoleModel>
    {
        public RoleRepository(IEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override async Task<RoleModel> GetByUniqueCode(string code)
        {
            var role = await Context.ApplicationRoles.FirstOrDefaultAsync(ar => ar.Name == code);
            if (role != null)
            {
                return Mapper.Map<RoleModel>(role);
            }

            throw new ObjectNotFoundException("Role not found");

        }

        public override Task Update(RoleModel model)
        {
            throw new NotImplementedException();
        }

        public override Task Delete(RoleModel model)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> Exists(string code)
        {
            return await Context.ApplicationRoles.AnyAsync(ar => ar.Name == code);
        }
    }
}