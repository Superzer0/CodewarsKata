namespace Kata.ClockFace
{
    using NUnit.Framework;

    [TestFixture]
    public class DinglemouseTest
    {
        private static object[] Example_Test_Cases = new object[]
        {
            new object[] {"12:00", 0},
            new object[] {"12:00", 360},
            new object[] {"03:00", 90},
            new object[] {"06:00", 180},
            new object[] {"09:00", 270},
            new object[] {"06:54", 207.25d},
            new object[] {"09:05", 272.86d},
        };
  
        [Test, TestCaseSource(typeof(DinglemouseTest), "Example_Test_Cases")]
        public void Example_Test(string expected, double test)
        {
            Assert.AreEqual(expected, Dinglemouse.WhatTimeIsIt(test));
        }
    }
}
