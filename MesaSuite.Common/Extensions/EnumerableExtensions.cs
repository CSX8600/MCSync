using System;
using System.Collections.Generic;

namespace MesaSuite.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable != null)
            {
                foreach (T item in enumerable)
                {
                    action(item);
                }
            }
        }

        public static IEnumerable<T> Edit<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable != null)
            {
                foreach (T item in enumerable)
                {
                    action(item);
                    yield return item;
                }
            }
        }
    }
}
