namespace JosephKata.MergedStringChecker
{
    public class StringMerger
    {
        public static bool isMerge(string s, string part1, string part2)
        {
            if (s == null)
                return false;

            var i = 0;
            var j = 0;

            foreach (var character in s)
            {
                if (CharacterIsContained(part1, character, ref i)) continue;
                if (CharacterIsContained(part2, character, ref j)) continue;

                // need to search for additional characters:

                if (GetNextCharacter(part1, character, ref i)) continue;
                if (GetNextCharacter(part2, character, ref j)) continue;

                return false;
            }

            return true;
        }

        private static bool GetNextCharacter(string part, char character, ref int index)
        {
            var newIndex = SearchForCharacter(part, character, index);
            if (newIndex <= index) return false;

            index = newIndex;
            return true;
        }

        private static int SearchForCharacter(string part, char character, int index)
        {
            return part.Substring(index).IndexOf(character) + index;
        }

        private static bool CharacterIsContained(string part, char character, ref int index)
        {
            if (part.Length > index && part[index] == character)
            {
                ++index;
                return true;
            }

            return false;
        }
    }
}