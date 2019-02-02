using System.Collections.Generic;
using System.Linq;

namespace JosephKata.NextBiggerNumber
{
    public class Kata
    {
        public static long NextBiggerNumber(long n)
        {
            var nAsString = n.ToString();
            var largestNumberPossible = long.Parse(
                string.Join(string.Empty, nAsString.OrderByDescending(p => p)));
            if (largestNumberPossible <= n)
                return -1;

            var allPermutations = GetPermutations(nAsString)
                .Distinct()
                .OrderBy(p => p)
                .ToList();

            var nIndex = allPermutations.IndexOf(nAsString);
            return long.Parse(allPermutations[nIndex + 1]);
        }

        private static IEnumerable<string> GetPermutations(string stringToPermutate)
        {
            var accumulator = new List<string>();
            var x = stringToPermutate.Length - 1;
            GetPer(stringToPermutate.ToCharArray(), 0, x, accumulator);
            return accumulator;
        }

        private static void GetPer(char[] list, int k, int m, List<string> accumulator)
        {
            if (k == m)
            {
                accumulator.Add(string.Concat(list));
            }
            else
            {
                for (var i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    GetPer(list, k + 1, m, accumulator);
                    Swap(ref list[k], ref list[i]);
                }
            }
        }

        private static void Swap(ref char a, ref char b)
        {
            if (a == b) return;

            a ^= b;
            b ^= a;
            a ^= b;
        }
    }
}