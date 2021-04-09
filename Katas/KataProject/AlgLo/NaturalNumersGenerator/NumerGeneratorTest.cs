using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace NaturalNumersGenerator
{
    [TestFixture]
    public class NumberGeneratorTests
    {
        [Test]
        public void GetSome()
        {
            var expectedCollection = new List<int> { 2, 4, 6, 8, 10, 12, 14 };
            var amountOfEvenNumers = 7;
            var results = NumbersGenerator.GetEvenNumers(1)
                         .Take(amountOfEvenNumers).ToList();
            CollectionAssert.AreEqual(expectedCollection, results);
        }
    }
}