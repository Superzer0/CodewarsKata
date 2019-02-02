using NUnit.Framework;

namespace JosephKata.MergedStringChecker
{
    [TestFixture]
    public class StringMergerTests
    {
        [Test]
        public void HappyPath1()
        {
            Assert.IsTrue(StringMerger.isMerge("codewars", "code", "wars"),
                "codewars can be created from code and wars");
        }

        [Test]
        public void HappyPath2()
        {
            Assert.IsTrue(StringMerger.isMerge("codewars", "cdwr", "oeas"),
                "codewars can be created from cdwr and oeas");
        }

        [Test]  
        public void SadPath1()
        {
            Assert.IsFalse(StringMerger.isMerge("codewars", "cod", "wars"), "Codewars are not codwars");
        }
    }
}