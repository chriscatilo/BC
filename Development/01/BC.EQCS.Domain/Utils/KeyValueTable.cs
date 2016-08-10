using System.Collections;
using System.Collections.Generic;

namespace BC.EQCS.Domain.Utils
{
    public abstract class KeyValueTable<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _transitions;

        protected KeyValueTable(IDictionary<TKey, TValue> transitions)
        {
            _transitions = transitions;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _transitions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _transitions.Contains(item);
        }

        public int Count { get { return _transitions.Count; } }

        public bool ContainsKey(TKey key)
        {
            return _transitions.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _transitions.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get { return _transitions[key]; }
        }

        public IEnumerable<TKey> Keys
        {
            get { return _transitions.Keys; }
        }

        public IEnumerable<TValue> Values
        {
            get { return _transitions.Values; }
        }
    }
}