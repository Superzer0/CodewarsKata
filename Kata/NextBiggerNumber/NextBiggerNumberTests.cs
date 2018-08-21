using NUnit.Framework;

namespace JosephKata.NextBiggerNumber
{
    [TestFixture]
    public class NextBiggerNumberTests
    {
        [Test]
        [TestCase(-1, 9)]
        [TestCase(-1, 111)]
        [TestCase(-1, 531)]
        [TestCase(21, 12)]
        [TestCase(531, 513)]
        [TestCase(2071, 2017)]
        [TestCase(441, 414)]
        [TestCase(414, 144)]
        [TestCase(1234567980, 1234567908)]
        public void Test1(int expected, int number)
        {
            Assert.AreEqual(expected, Kata.NextBiggerNumber(number));
        }
    }
}