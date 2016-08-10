using System.Linq;
using System.Security.Cryptography;

namespace RoleSwitcher.Repository
{
    public class UserRoleRepository
    {
        public bool Exists(int userId, int roleId, int adminUnitId)
        {
            using (var context = new EQCSEntities())
            {
                return context.UserToRoleToAdminUnits.Where(
                    x =>
                        x.ApplicationUserId == userId && x.ApplicationRoleId == roleId &&
                        x.AdminUnitId == adminUnitId).Any();
            }
        }

        public void DeleteUserRole(int userId, int roleId, int adminUnitId)
        {
            using (var context = new EQCSEntities())
            {


                var userRole =
                    context.UserToRoleToAdminUnits.Where(
                        x =>
                            x.ApplicationUserId == userId && x.ApplicationRoleId == roleId &&
                            x.AdminUnitId == adminUnitId);

                context.UserToRoleToAdminUnits.RemoveRange(userRole);


                context.SaveChanges();


            }
        }

        public void AddUserRole(int userId, int roleId, int adminUnitId)
        {
            using (var context = new EQCSEntities())
            {
                context.UserToRoleToAdminUnits.Add(
                    new UserToRoleToAdminUnit()
                    {
                        ApplicationUserId = userId,
                        ApplicationRoleId = roleId,
                        AdminUnitId = adminUnitId
                    });

                context.SaveChanges();
            }
        }

        public bool IsAllowed(int roleId, int adminUnitId)
        {
            using (var context = new EQCSEntities())
            {
                return (from role in context.ApplicationRoles
                        where role.Id == roleId
                        from adminUnitType in role.AdminUnitTypes
                        where adminUnitType.Id == (
                                                    from adminUnit in context.AdminUnits 
                                                    where adminUnit.Id == adminUnitId 
                                                    select adminUnit.AdminUnitType.Id 
                                                   ).FirstOrDefault()
                        select role).Any();
            }
        }
    }
}