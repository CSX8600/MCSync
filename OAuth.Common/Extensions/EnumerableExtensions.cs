using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach(T item in enumerable)
            {
                action(item);
            }

            return enumerable;
        }

        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> enumerable, int batchSize = 2000)
        {
            if (enumerable.Count() <= batchSize)
            {
                yield return enumerable;
            }
            else
            {
                for(int i = 0; i < enumerable.Count(); i += batchSize)
                {
                    yield return enumerable.Skip(i).Take(batchSize);
                }
            }
        }
    }
}