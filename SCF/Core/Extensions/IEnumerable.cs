using Peyton.Core.Repository;
using System.Linq;

// ReSharper disable once CheckNamespace

namespace System.Collections.Generic
{
    // ReSharper disable once InconsistentNaming
    public static class IEnumerableExt
    {
        public static List<T> ToListExt<T>(this IEnumerable<IEnumerable<T>> list)
        {
            var result = new List<T>();
            foreach (var item in list)
                result.AddRange(item);
            return result;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
                if (seenKeys.Add(keySelector(element)))
                    yield return element;
        }

        public static List<string> Trim(this IEnumerable<string> value)
        {
            if (value == null)
                return new List<string>();
            return value.Where(i => !string.IsNullOrWhiteSpace(i)).Distinct().ToList();
        }

        public static string Combine(this IEnumerable<long> value, string separator)
        {
            return value.Select(i => i.ToString()).ToList().Combine(separator);
        }

        
    }
}