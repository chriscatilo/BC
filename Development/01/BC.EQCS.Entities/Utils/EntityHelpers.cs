using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FastMember;

namespace BC.EQCS.Entities.Utils
{
    public class EntityHelpers
    {
        /// <summary>
        /// Get the property names of the entity appended by 'Id' and a matching property not appended by 'Id' (i.e. AddressCountryId and AddressCountry)
        /// </summary>
        public static IEnumerable<IdentifierReferencePair> GetIdReferencePairsOf<TEntity>()
        {
            Func<Type, IEnumerable<IdentifierReferencePair>> getIdRefPairs = type =>
                {
                    var accessor = GetTypeAccessor<TEntity>();

                    var members = accessor.GetMembers().ToDictionary(m => m.Name);

                    // get entity properties with 'Id' at the end
                    Regex withIdPattern = new Regex(@"^(.*)Id$");
                    var propertyNamesWithId = members.Where(kvp => withIdPattern.IsMatch(kvp.Key))
                                                     .Select(kvp => kvp.Key);

                    var idRefPairs
                        = propertyNamesWithId.Join(members.Keys, // <= inner
                                                   inner => withIdPattern.Replace(inner, "$1"),
                                                   outer => outer,
                                                   (inner, outer) => new IdentifierReferencePair
                                                   {
                                                       IdentifierProperty = inner,
                                                       ReferrenceProperty = outer
                                                   })
                                             .Where(pair => members.ContainsKey(pair.ReferrenceProperty));

                    return idRefPairs;
                };

            Type entityType = typeof(TEntity);

            IEnumerable<IdentifierReferencePair> identifierReferencePairs;

            if (StaticLookupCache.TypePairLookup.TryGetValue(entityType.FullName, out identifierReferencePairs))
            {
                return identifierReferencePairs;
            }

            identifierReferencePairs = getIdRefPairs(entityType);

            // ReSharper disable PossibleMultipleEnumeration

            StaticLookupCache.TypePairLookup.Add(entityType.FullName, identifierReferencePairs);

            return identifierReferencePairs;

            // ReSharper restore PossibleMultipleEnumeration
        }
        
        internal static TypeAccessor GetTypeAccessor<T>()
        {
            var type = typeof (T);
            TypeAccessor typeAccessor;
            if (StaticLookupCache.TypeAccessors.TryGetValue(type.FullName, out typeAccessor))
            {
                return typeAccessor;
            }

            typeAccessor = TypeAccessor.Create(type);
            StaticLookupCache.TypeAccessors.Add(type.FullName, typeAccessor);
            return typeAccessor;
        }
    }
}
