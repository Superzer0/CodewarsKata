using System;

namespace KataProject.CSharpTutorial.Tutorials;

public class NullableTutorial
{
    public string SomeProperty { get; set; }


    public void Run()
    {
        SomeProperty = null;
        string NonNullableVariable = "null";
        NonNullableVariable = null; // generates warning
        
        
        string? NullableVariable = null;
        var ImplicitlyNullableVariable = "(string)null;";
        ImplicitlyNullableVariable = null; // no warning here as types defined with var are nullable. 
        
        // null analysis pitfalls: no warning but null ref exception here 
        string[] values = new string[10];
        string s = values[0];
        Console.WriteLine(s.ToUpper());
        
    }
    
    // structs! 
    /*
     *Within a readonly instance member, you can't assign to structure's instance fields. However, a readonly member can call a non-readonly member. In that case the compiler creates a copy of the structure instance and calls the non-readonly member on that copy. As a result, the original structure instance is not modified.
     * 
     */

    public struct Foo
    {
        
    }
    
}