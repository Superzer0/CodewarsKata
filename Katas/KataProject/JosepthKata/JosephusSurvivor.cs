using System.Linq;

namespace JosephKata
{
    internal class JosephusSurvivor
    {
        public static int JosSurvivor(int n, int k)
        {
            var list = Enumerable.Range(1, n).ToList();
            var index = (k - 1) % list.Count;
            while (list.Count != 1)
            {
                if (index < 0)
                {
                    index = list.Count + index - 1;
                }

                list.RemoveAt(index);
                index = (index + k - 1) % list.Count;
            }

            return list.First();
        }
    }
}