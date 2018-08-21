using NUnit.Framework;

namespace Kata.unique_in_order
{
    [TestFixture]
    public class SolutionTest
    {
        [Test]
        public void EmptyTest()
        {
            Assert.AreEqual("", JosephKata.unique_in_order.Kata.UniqueInOrder(""));
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual("ABCDAB", JosephKata.unique_in_order.Kata.UniqueInOrder("AAAABBBCCDAABBB"));
        }
    }
}