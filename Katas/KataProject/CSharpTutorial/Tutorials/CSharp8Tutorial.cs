using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions.Formatting;

namespace Kata.CSharpTutorial.Eight
{
    // Readonly members in struct 
    // ref struct will never leave the stack 
    public ref struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public readonly int Square => X * Y;

        public readonly override string ToString()
        {
            // without readonly in  public readonly int Square => X * Y; 
            // compiler will generate a warning 

            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Square)}: {Square}";
        }

        // this will deconstruct tuple. It can be instance method 
        // it can be extension method as well 
        public void Deconstruct(out int X, out int Y)
        {
            X = this.X;
            Y = this.Y;
        }

        public static void DeconstructExample()
        {
            var point = new Point();
            var (x, y) = point;
            Console.WriteLine($"X: {x}, Y: {y}");
        }
    }

    // default interface methods in C# 8.0 
    // interfaces can now have static fields and any access modifier (public, private, protected) 

    public interface ICustomer
    {
        public const string MyConst = "MyStaticConst";
        private const string MyHiddenConst = "Secret I will never tell xO x0";
        void Foo();
        public void Bar();

        public void FooBar()
        {
            Console.WriteLine("This is default implementation of FooBar. DealWithIt");
        }

        public static string PrintConsts()
        {
            return $" {MyConst} {MyHiddenConst}";
        }
    }

    public class CustomerImplementation : ICustomer
    {
        public void Foo()
        {
            throw new NotImplementedException();
        }

        public void Bar()
        {
            throw new NotImplementedException();
        }

        public void FooBar() // no override keyword here
        {
            Console.WriteLine("This is overriden implementation of FooBar. Nothing to show off");
        }

        public static void PlayWithICustomer(CustomerImplementation customerImplementation)
        {
            customerImplementation.FooBar();
            Console.WriteLine(ICustomer.MyConst); // read const directly from the interface type
            ICustomer customer = customerImplementation;
            Console.WriteLine(ICustomer.PrintConsts()); // static methods only from interface 
            customer.FooBar(); //

            // static local functions 
            // now we are sure with static that nothing will be captured by this local function
            static void PrintICustomer(ICustomer customer)
            {
                Console.WriteLine(ICustomer.PrintConsts()); // static methods only from the instance ?? 
                customer.FooBar(); //
            }
        }
    }

    // patter matching 
    // Switch expressions

    public enum Rainbow
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Violet
    }

    public class PatternMatchExamples
    {
        public static string ToTextSwitch(Rainbow rainbow) => rainbow switch
        {
            Rainbow.Blue => "STH is blue",
            Rainbow.Green => "Grass is green",
            Rainbow.Violet => "Violet sky",
            _ => "Default option"
        };

        public static string ToTextSwitchProperty(Point point) => point switch
        {
            {Square : 1} => "This is square 1", // we check on specific field of the object
            {Square: 2} => "This is square 2",
            {X: 2} => "This is X", // this property can be different,

            _ => "this is default"
        };

        public static string RockPaperScissors(string first, string second)
            => (first, second) switch
            {
                ("rock", "paper") => "rock is covered by paper. Paper wins.",
                ("rock", "scissors") => "rock breaks scissors. Rock wins.",
                ("paper", "rock") => "paper covers rock. Paper wins.",
                ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
                ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
                ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
                (_, _) => "tie"
            };
    }


    /// <summary>
    /// The difference is that with the new IAsyncEnumerable you can consume your stream without waiting for whole collection.
    /// With IEnumerable it is hard to have async methods and yield from collection as you get results.
    /// When the output is IEnumerable from task you must await task and get whole collection but with IAsyncEnumerable you are just iterating and
    /// processing results as you go. 
    /// </summary>
    public static class AsyncInteratorsExample
    {
        private static readonly Random _random = new();

        private static async IAsyncEnumerable<int> GenerateSequence()
        {
            for (var i = 0; i < 20; i++)
            {
                var timeToWait = _random.Next(50, 200);
                await Task.Delay(timeToWait);
                yield return i;
            }
        }

        private static async Task<IEnumerable<int>> GenerateSequenceOldWay()
        {
            var list = new List<int>();
            for (var i = 0; i < 20; i++)
            {
                var timeToWait = _random.Next(50, 200);
                await Task.Delay(timeToWait);
                list.Add(i);
            }

            return list;
        }

        public static async Task RunWithNormalCollection()
        {
            var sequence = await GenerateSequenceOldWay();
            foreach (var item in sequence)
            {
                Console.WriteLine(item);
            }

            List<int>.Enumerator en = new List<int>().GetEnumerator();
        }

        public static async Task RunWithAsyncIterator()
        {
            await foreach (var number in GenerateSequence())
            {
                Console.WriteLine(number);
            }
        }
    }

    public class NullCoalescingOperator
    {
        public void Run()
        {
            List<int> list = null;
            int? i = null;

            list ??= new List<int>();
            list.Add(i ??= 1);
            list.Add(i ??= 2);

            Console.WriteLine(string.Join(" ", list));
        }
    }

    // IAsyncDisposable. You implement both async and non-async disposable 
    class ExampleConjunctiveDisposableusing : IDisposable, IAsyncDisposable
    {
        IDisposable _disposableResource = new MemoryStream();
        IAsyncDisposable _asyncDisposableResource = new MemoryStream();

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore().ConfigureAwait(false);

            Dispose(disposing: false);
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
            GC.SuppressFinalize(this);
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _disposableResource?.Dispose();
                (_asyncDisposableResource as IDisposable)?.Dispose();
                _disposableResource = null;
                _asyncDisposableResource = null;
            }
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (_asyncDisposableResource is not null)
            {
                await _asyncDisposableResource.DisposeAsync().ConfigureAwait(false);
            }

            if (_disposableResource is IAsyncDisposable disposable)
            {
                await disposable.DisposeAsync().ConfigureAwait(false);
            }
            else
            {
                _disposableResource?.Dispose();
            }

            _asyncDisposableResource = null;
            _disposableResource = null;
        }
    }
}

internal class ReadOnlyEnumerator
{
    // readonly struct can be evil ! 
    // each time it is accessed, there will be a defensive copy created :( 
    private readonly List<int>.Enumerator _enumerator;

    public ReadOnlyEnumerator(List<int> list)
    {
        Contract.Requires(list.Count >= 1);
        _enumerator = list.GetEnumerator();
    }

    public void PrintTheFirstElement()
    {
        _enumerator.MoveNext();
        Console.WriteLine(_enumerator.Current);
    }

    public static void Run()
    {
        var roe = new ReadOnlyEnumerator(new List<int> {1, 2});
        roe.PrintTheFirstElement();
    }
}

// http://mustoverride.com/refs-not-ptrs/
// ref returns and ref locals work for me as pointers but they are not. They are aliases. 
// Everything is passed by default as value (references for classes, whole objects for value types) 
// and ref changes that. 
// ref int x = ref arr[1]; does not mean that ref int is a pointer. It just says that x does not have its own storage. 
// it points to the same location. 
// ref returns & ref locals 
// ref VeryLargeStruct reflocal = ref veryLargeStruct;

// ref VeryLargeStruct reflocal = ref veryLargeStruct; // initialization
// refLocal = ref anotherVeryLargeStruct; // reassigned, refLocal refers to different storage.
class NumberStore
{
    int[] numbers = { 1, 3, 7, 15, 31, 63, 127, 255, 511, 1023 };

    public ref int FindNumber(int target)
    {
        for (int ctr = 0; ctr < numbers.Length; ctr++)
        {
            if (numbers[ctr] >= target)
                return ref numbers[ctr];
        }
        return ref numbers[0];
    }

    public override string ToString() => string.Join(" ", numbers);

    public static void Run()
    {
        var store = new NumberStore();
        Console.WriteLine($"Original sequence: {store.ToString()}");
        int number = 16;
        ref var value = ref store.FindNumber(number);
        value *= 2;
        Console.WriteLine($"New sequence:      {store.ToString()}");
        // The example displays the following output:
        //       Original sequence: 1 3 7 15 31 63 127 255 511 1023
        //       New sequence:      1 3 7 15 62 63 127 255 511 1023\

        Console.WriteLine("**********");
        int[] numbers = { 1, 3, 7};
        ref var x = ref numbers[1];
        Console.WriteLine(x);
        var y = x; // normal copy it, y is not changed 
        x = 5;
        Console.WriteLine(x); // 5
        Console.WriteLine(numbers[1]); // 5
        Console.WriteLine(y); // 3
    }
}


record class NumbersWrapper(int Number);
class NumberStoreWithClasses
{
    public NumberStoreWithClasses()
    {
        NumbersWrappers = numbers.Select(p => new NumbersWrapper(p)).ToArray();
    }
    
    int[] numbers = { 1, 3, 7, 15, 31, 63, 127, 255, 511, 1023 };
    public NumbersWrapper[] NumbersWrappers { get; init; }
    
    public ref NumbersWrapper FindNumber(int target)
    {
        var wrappers = NumbersWrappers;
        for (int ctr = 0; ctr < wrappers.Length; ctr++)
        {
            if (wrappers[ctr].Number >= target)
                return ref wrappers[ctr];
        }
        return ref wrappers[0];
    }

    public override string ToString() => string.Join(" ", NumbersWrappers.Select(p => p.Number));

    public static void Run()
    {
        var store = new NumberStoreWithClasses();
        ref var store1 = ref store; 
        
        Console.WriteLine($"Original sequence: {store.ToString()}");
        int number = 16;
        ref var value = ref store.FindNumber(number);
        value = value with {Number = 2 };
        Console.WriteLine($"New sequence:      {store.ToString()}");
        // The example displays the following output:
        //       Original sequence: 1 3 7 15 31 63 127 255 511 1023
        //       New sequence:      1 3 7 15 62 63 127 255 511 1023
    }
}
