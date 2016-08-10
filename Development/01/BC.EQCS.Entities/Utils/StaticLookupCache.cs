using System.Collections.Concurrent;
using System.Collections.Generic;
using FastMember;

namespace BC.EQCS.Entities.Utils
{
    internal static class StaticLookupCache
    {
        public static readonly IDictionary<string, IEnumerable<IdentifierReferencePair>> TypePairLookup;

        public static readonly IDictionary<string, TypeAccessor> TypeAccessors;

        static StaticLookupCache()
        {
            TypePairLookup = new ConcurrentDictionary<string, IEnumerable<IdentifierReferencePair>>();
            TypeAccessors = new ConcurrentDictionary<string, TypeAccessor>();
        }
    }
}