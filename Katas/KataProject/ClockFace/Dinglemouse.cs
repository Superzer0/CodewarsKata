using System;

namespace Kata.ClockFace
{
    /// <summary>
    /// https://www.codewars.com/kata/59752e1f064d1261cb0000ec/train/csharp
    /// </summary>
    public class Dinglemouse
    {
        public static string WhatTimeIsIt(double angle)
        {
            return (new DateTime() + new TimeSpan(0, 0, (int)Math.Floor(angle * 2), 0)).ToString("hh:mm");
        }
    }
}
