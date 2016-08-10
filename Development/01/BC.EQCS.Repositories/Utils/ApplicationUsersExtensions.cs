using System.Data.Entity;
using System.Linq;
using BC.EQCS.Entities.Models;

namespace BC.EQCS.Repositories.Utils
{
    public static class ApplicationUsersExtensions
    {
        public static IQueryable<ApplicationUser> IncludeAdminUnits(this IQueryable<ApplicationUser> source)
        {
            return source.Include(x => x.UserToRoleToAdminUnits.Select(y => y.AdminUnit.Type));
        }


        public static IQueryable<ApplicationUser> IncludeApplicationAssets(this IQueryable<ApplicationUser> source)
        {
            return source.Include(x => x.UserToRoleToAdminUnits.Select(y => y.ApplicationRole.ApplicationAssets));
        }

        public static IQueryable<ApplicationUser> IncludeIncidentClasses(this IQueryable<ApplicationUser> source)
        {
            return
                source.Include(
                    x => x.UserToRoleToAdminUnits.Select(y => y.ApplicationRole.IncidentClasses.Select(z => z.Type)));
        }

        public static IQueryable<ApplicationUser> IncludeReadableIncidentClasses(this IQueryable<ApplicationUser> source)
        {
            return
                source.Include(
                    x => x.UserToRoleToAdminUnits.Select(y => y.ApplicationRole.ViewableIncidentClasses.Select(z => z.Type)));
        }

        public static IQueryable<ApplicationUser> IncludeReadOnlyIncidentClasses(this IQueryable<ApplicationUser> source)
        {
            return
                source.Include(
                    x => x.UserToRoleToAdminUnits.Select(y => y.ApplicationRole.ReadOnlyIncidentClasses.Select(z => z.Type)));
        }
    }
}
