using System;

/*
https://www.codewars.com/kata/5517fcb0236c8826940003c9/train/csharp
*/
public class SumFractions
{
    public static string SumFracts(int[,] l)
    {
        if(l.Length == 0){
            return string.Empty;
        }

        var denominator = l[0, 1];
        for (int i = 1; i < l.GetLength(0); i++)
        {
            denominator = Nww(denominator, l[i, 1]);
        }

        var sum = 0;
        for (int i = 0; i < l.GetLength(0); i++)
        {
            sum += l[i, 0] * (denominator / l[i, 1]);
        }

        var fractionNwd = Nwd(sum, denominator);
        var resultCounter = sum / fractionNwd;
        var resultDenominator = denominator / fractionNwd;

        return resultCounter % resultDenominator == 0
            ? (resultCounter / resultDenominator).ToString()
            : $"[{sum / fractionNwd}, {denominator / fractionNwd}]";
    }


    public static int Nwd(int a, int b)
    {
        if (a == 0)
        {
            return b;
        }

        if (b == 0)
        {
            return a;
        }

        while (a != b)
        {
            if (a < b)
            {
                b = b - a;
            }
            else
            {
                a = a - b;
            }
        }
        return a;
    }

    public static int Nww(int a, int b)
    {
        return (a * b) / Nwd(a, b);
    }

}