using System.Collections.Generic;

namespace GuardedActions.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddOrCreate<TKey, TValue>(this SortedDictionary<TKey, IList<TValue>> dictionary, TKey key, TValue value)
        {
            if (dictionary == null)
                return;

            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, new List<TValue> { value });
            else
                dictionary[key].Add(value);
        }
    }
}
