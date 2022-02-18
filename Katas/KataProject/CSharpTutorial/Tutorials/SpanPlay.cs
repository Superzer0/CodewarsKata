using System;
using NUnit.Framework;


namespace KataProject.CSharpTutorial.Tutorials;

public class SpanPlay
{
    public static void Run()
    {
        var arr = new byte[10];
        Span<byte> bytes = arr; // Implicit cast from T[] to Span<T>
        
        Span<byte> slicedBytes = bytes.Slice(start: 5, length: 2);
        slicedBytes[0] = 42;
        slicedBytes[1] = 43;
        Assert.Equals(42, slicedBytes[0]);
        Assert.Equals(43, slicedBytes[1]);
        Assert.Equals(arr[5], slicedBytes[0]);
        Assert.Equals(arr[6], slicedBytes[1]);
        slicedBytes[2] = 44; // Throws IndexOutOfRangeException
        bytes[2] = 45; // OK
        Assert.Equals(arr[2], bytes[2]);
        Assert.Equals(45, arr[2]);
        
        
        Span<byte> bytesStack = stackalloc byte[2]; // Using C# 7.2 stackalloc support for spans
        bytesStack[0] = 42;
        bytesStack[1] = 43;
        Assert.Equals(42, bytesStack[0]);
        Assert.Equals(43, bytesStack[1]);
        bytesStack[2] = 44; // throws IndexOutOfRangeException
        
    }
}