using System;
using NUnit.Framework;

[TestFixture]
public class SumFractionsTests
{
    private static void testing(string actual, string expected)
    {
        Assert.AreEqual(expected, actual);
    }

    [Test]
    //[Ignore("NotReady yet")]
    public void test()
    {
        Console.WriteLine("Fixed Tests");
        int[,] a = new int[,] { { 1, 2 }, { 2, 9 }, { 3, 18 }, { 4, 24 }, { 6, 48 } };
        String r = "[85, 72]";
        testing(SumFractions.SumFracts(a), r);
        a = new int[,] { { 1, 2 }, { 1, 3 }, { 1, 4 } }; 
        r = "[13, 12]";
        testing(SumFractions.SumFracts(a), r);
        a = new int[,] { { 1, 3 }, { 5, 3 } };
        r = "2";
        testing(SumFractions.SumFracts(a), r);
        a = new int[,] { };
        r = null;
    }

    [Test]
    [TestCase(1,2,1)]
    [TestCase(1,1,1)]
    [TestCase(10,20,10)]
    [TestCase(40,45,5)]
    [TestCase(192,348,12)]
    [TestCase(42,56,14)]
    public void TestNwd(int a, int b, int expected)
    {
        var result = SumFractions.Nwd(a, b);
        Assert.AreEqual(expected, result);

        int[,]  c = new int[,] { { 1, 2 }, { 2, 9 }, { 3, 18 }, { 4, 24 }, { 6, 48 } };
        
    }

    [Test]
    [TestCase(24,36,72)]
    public void TestNww(int a, int b, int expected)
    {
        var result = SumFractions.Nww(a, b);
        Assert.AreEqual(expected, result);
    }
}