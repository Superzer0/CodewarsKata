using NUnit.Framework;

namespace JosephKata
{
    [TestFixture]
    public class JosephusSurvivorTests
    {
        private static void Testing(int actual, int expected)
        {
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(7, 3, 4)]
        [TestCase(11, 19, 10)]
        [TestCase(40, 3, 28)]
        [TestCase(14, 2, 13)]
        [TestCase(100, 1, 100)]
        [TestCase(1, 300, 1)]
        [TestCase(2, 300, 1)]
        [TestCase(5, 300, 1)]
        [TestCase(7, 300, 7)]
        [TestCase(300, 300, 265)]
        public void Test(int n, int k, int expected)
        {
            Testing(JosephusSurvivor.JosSurvivor(n, k), expected);
        }
    }
}