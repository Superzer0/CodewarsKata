using System.Collections.Generic;

namespace NaturalNumersGenerator
{
    public static class NumbersGenerator
    {
        public static IEnumerable<int> GetEvenNumers(int start)
        {
            if (start <= 0)
            {
                yield break;
            }

            var cursor = start;
            if (cursor % 2 != 0)
            {
                cursor += 1;
            }

            while (true)
            {
                yield return cursor;
                cursor += 2;
            }
        }
    }
}