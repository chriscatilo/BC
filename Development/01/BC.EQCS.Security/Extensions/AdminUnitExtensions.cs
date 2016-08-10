using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Models;
using BC.EQCS.Utils;

namespace BC.EQCS.Security.Extensions
{
    public static class AdminUnitExtensions
    {
        public static bool CanAccess(this AdminUnitModel adminUnit, string adminUnitCode)
        {
            if (adminUnit.Code.EqualsCaseInsensitive(adminUnitCode))
            {
                return true;
            }

            var canAccess = CanAccessDescendant(adminUnit.Children, adminUnitCode);

            return canAccess.HasValue && canAccess.Value;
        }

        private static bool? CanAccessDescendant(IEnumerable<AdminUnitModel> adminUnits, string adminUnitCode)
        {
            foreach (var adminUnit in adminUnits)
            {
                if (adminUnit.Code.EqualsCaseInsensitive(adminUnitCode))
                {
                    return true;
                }

                if (adminUnit.Children.Count() != 0)
                {
                    var canAccess = CanAccessDescendant(adminUnit.Children, adminUnitCode);

                    if (canAccess.HasValue && canAccess == true)
                    {
                        return true;
                    }
                }
                
            }

            return null;
        }

        //TODO: Stick this in its own file
        public static IEnumerable<AdminUnitModel> GetDescendants(this AdminUnitModel adminUnit)
        {
            var response = new List<AdminUnitModel> {adminUnit};

            AddChildrenToCollection(adminUnit.Children, response);

            return response.GroupBy(g => g.Code)
                .Select(grp => grp.First())
                .ToList();
        }

        private static void AddChildrenToCollection(IEnumerable<AdminUnitModel> adminUnits,
            List<AdminUnitModel> response)
        {
            foreach (var adminUnit in adminUnits)
            {
                response.Add(adminUnit);

                AddChildrenToCollection(adminUnit.Children, response);
            }
        }

        public static AdminUnitModel GetAdminUnitFromTree(this AdminUnitModel adminUnit, string code)
        {
            AdminUnitModel response = null;

            if (adminUnit.Code == code)
            {
                return adminUnit;
            }

            foreach (var adminUnitModel in adminUnit.Children)
            {
                response = GetAdminUnitFromTree(adminUnitModel, code);

                if (response != null)
                {
                    break;
                }
            }

            return response;

        }

        public static IEnumerable<AdminUnitModel> GetByTypeCode(this AdminUnitModel adminUnit, string adminTypeCode)
        {
            var response = new List<AdminUnitModel>();

            if (adminUnit.Type.Equals(adminTypeCode))
            {
                response.Add(adminUnit);
                return response;
            }

            if (adminUnit.Children != null)
            {
                foreach (var subGeoArea in adminUnit.Children)
                {
                    response.AddRange(GetByTypeCode(subGeoArea, adminTypeCode));
                }
            }

            return response;
        }
    }
}