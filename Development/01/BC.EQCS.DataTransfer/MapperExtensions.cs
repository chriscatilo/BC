using System;
using AutoMapper;
using BC.EQCS.Entities.Models;
using BC.EQCS.Entities.Utils;

namespace BC.EQCS.DataTransfer
{
    public static class MapperExtensions
    {
        /// <summary>
        /// Do not map reference properties of an entity from matching property values in model as their 
        /// values should be assigned within the repository.
        /// </summary>
        /// <typeparam name="TModel">e.g. Models.PersonModel</typeparam>
        /// <typeparam name="TEntity">e.g. Models.Person</typeparam>
        public static IMappingExpression<TModel, TEntity> IgnoreEntityRelations<TModel, TEntity>(
            this IMappingExpression<TModel, TEntity> mapper)
        {
            var idRefPairs = EntityHelpers.GetIdReferencePairsOf<TEntity>();

            foreach (var pair in idRefPairs)
            {
                mapper.ForMember(pair.ReferrenceProperty, options => options.Ignore());

                mapper.ForMember(pair.IdentifierProperty, options => options.Ignore());
            }
            return mapper;
        }

        // TODO Chris: refactor GetAscendantAdminUnitCode and GetAscendantIncidentClassCode into one

        /// <summary>
        /// Traverse up the administrative structure of a test location to get the admin unit code of a given type
        /// </summary>
        public static string GetAscendantAdminUnitCode(this TestLocation location, string adminUnitType)
        {
            if (location == null)
            {
                return null;
            }

            var adminUnit = location.AdminUnit;
            while (adminUnit != null && !adminUnit.Type.Code.Equals(adminUnitType, StringComparison.InvariantCultureIgnoreCase))
            {
                adminUnit = adminUnit.Parent;
            }

            return adminUnit == null ? null : adminUnit.Code;
        }

        /// <summary>
        /// Traverse up the incident structure of an incident ot get the ascendant of a given type
        /// </summary>
        public static string GetAscendantIncidentClassCode(this IncidentClass incidentClass, string nodeType)
        {
            if (incidentClass == null)
            {
                return null;
            }

            while (incidentClass != null && !incidentClass.Type.Code.Equals(nodeType, StringComparison.InvariantCultureIgnoreCase))
            {
                incidentClass = incidentClass.Parent;
            }

            return incidentClass == null ? null : incidentClass.Code;
        }

        /// <summary>
        /// Present the referring organisation as either the selected organisation or the literal name stored
        /// </summary>
        public static string GetReferringOrganisation(this Incident incident)
        {
            return incident.ReferringOrganisation == null
                ? incident.ReferringOrgName
                : incident.ReferringOrganisation.Code;
        }

        /// <summary>
        /// True if ReferringOrganisation is from incident.ReferringOrganisation
        /// False if from incident.ReferringOrgName
        /// Null if both incident.ReferringOrganisation & 
        /// </summary>
        public static bool? GetReferringOrgExists(this Incident incident)
        {
            return incident.ReferringOrganisation != null
                ? true
                : incident.ReferringOrgName == null
                    ? default(bool?)
                    : false;
        }
    }
}