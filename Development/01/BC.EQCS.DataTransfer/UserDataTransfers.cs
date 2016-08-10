using System.Linq;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;
using BC.EQCS.Security.Models;

namespace BC.EQCS.DataTransfer
{
    public static class UserDataTransfers
    {
        public static void PushValues(this ApplicationUser entity, SecurityUserModel model)
        {
            Mapper.Map(entity, model);

            model.ApplicationRoles = entity.UserToRoleToAdminUnits.Select(item =>
            {
                var role = Mapper.Map<RoleModel>(item.ApplicationRole);
                role.AdminUnit = Mapper.Map<AdminUnitModel>(item.AdminUnit);
                return role;
            }).ToList();
        }
    }
}