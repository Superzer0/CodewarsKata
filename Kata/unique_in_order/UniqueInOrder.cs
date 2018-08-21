using System.Collections.Generic;

namespace JosephKata.unique_in_order
{
    public static class Kata
    {
        public static IEnumerable<T> UniqueInOrder<T>(IEnumerable<T> iterable)
        {
            return UniqueInOrderImpl(iterable);
        }

        private static IEnumerable<T> UniqueInOrderImpl<T>(IEnumerable<T> iterable)
        {
            dynamic lastValue = default(T);
            foreach (var item in iterable)
            {
                if (lastValue == item)
                    continue;

                lastValue = item;
                yield return item;
            }
        }
    }
}