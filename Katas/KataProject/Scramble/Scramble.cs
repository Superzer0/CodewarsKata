using System.Collections.Generic;
using System.Linq;

public class Scramblies
{
    public static bool Scramble(string str1, string str2)
    {
        var dictionaryStr1 = ToOccurrences(str1);
        var dictionaryStr2 = ToOccurrences(str2);

        foreach (var charOccurrence in dictionaryStr2)
        {
            var value = 0;
            if (!dictionaryStr1.TryGetValue(charOccurrence.Key, out value))
                return false;

            if (value < charOccurrence.Value)
                return false;
        }

        return true;
    }

    private static Dictionary<string, int> ToOccurrences(string str)
    {
        var dictionary = new Dictionary<string, int>();
        var keyValuePairs = str.GroupBy(p => p, (c, enumerable)
            => new KeyValuePair<string, int>(c.ToString(), enumerable.Count())).ToList();

        foreach (var keyValuePair in keyValuePairs)
        {
            dictionary.Add(keyValuePair.Key, keyValuePair.Value);
        }

        return dictionary;
    }
}