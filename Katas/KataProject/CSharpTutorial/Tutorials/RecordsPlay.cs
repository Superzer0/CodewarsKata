using System;

namespace KataProject.CSharpTutorial.Tutorials;

// reference types that are immutable 
public record Person(string FirstName, string LastName);


/*
 *A record can inherit from another record. However, a record can't inherit from a class, and a class can't inherit from a record
 *
 * 
 */
public static class RecordsPlay
{
    public static void Run()
    {
        var person = new Person("J", "K");
        // person.FirstName = "J"; init only property but auto implemented properties  
        var (first, last) = person; // autoimplemented deconstruction. 

        // i can create new record 
        var anotherPerson = person with {FirstName = "K"};
        Console.WriteLine(anotherPerson);
        

    }
}