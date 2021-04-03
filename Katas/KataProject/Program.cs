using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Kata
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            int[,] fractions = new int[,] { { 1, 2 }, { 1, 3 }, { 1, 4 } };
            var output = SumFractions.SumFracts(fractions);
            Console.WriteLine(output);

        }

        static async Task ResumeWithoutContextAsync([CallerFilePath] string file = null, [CallerMemberName] string memberName = null)
        {
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(true);
            Console.WriteLine("ManagedThreadId {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine($"File: {file} called from {memberName}");
            // This method discards its context when it resumes.
        }
    }

    class MySynchornizationContext : SynchronizationContext
    {
        public override void Post(SendOrPostCallback d, object state)
        {
            Console.WriteLine("posted");
            base.Post(d, state);
        }

        public override void Send(SendOrPostCallback d, object state)
        {
            Console.WriteLine("sent");
            base.Send(d, state);
        }
    }
}