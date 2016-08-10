using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BC.EQCS.Domain.Utils
{
    public abstract class KeyValueLookup<TKey, TValue> : ILookup<TKey, TValue>
    {
        private readonly ILookup<TKey, TValue> _transitions;

        public KeyValueLookup(ILookup<TKey, TValue> transitions)
        {
            _transitions = transitions;
        }

        public IEnumerator<IGrouping<TKey, TValue>> GetEnumerator()
        {
            return _transitions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(TKey key)
        {
            return _transitions.Contains(key);
        }

        public int Count { get; private set; }

        public IEnumerable<TValue> this[TKey key]
        {
            get { return _transitions[key]; }
        }
    }
}