using System.Collections.Generic;
using System.Text;

namespace Kata.NStrings
{
    class SolutionJumbledString
    {
        public static string jumbledString(string s, long n)
        {
            var variationsList = new List<string>(s.Length) { s };

            var stringUnderProcessing = s;
            for (var i = 0; i < n; i++)
            {
                stringUnderProcessing = DoWork(stringUnderProcessing);
                if (stringUnderProcessing == s)
                    break;

                variationsList.Add(stringUnderProcessing);
            }

            if (n == variationsList.Count) return stringUnderProcessing;

            var index = (int)(n % variationsList.Count);
            return variationsList[index];
        }

        private static readonly StringBuilder StringBuilderEven = new StringBuilder();
        private static readonly StringBuilder StringBuilderOdd = new StringBuilder();

        private static string DoWork(string s)
        {
            StringBuilderEven.Clear();
            StringBuilderOdd.Clear();

            var isEven = true;

            foreach (var c in s)
            {
                if (isEven)
                {
                    StringBuilderEven.Append(c);
                }
                else
                {
                    StringBuilderOdd.Append(c);
                }

                isEven = !isEven;
            }

            return StringBuilderEven + StringBuilderOdd.ToString();
        }
    }
}

