using System;
using System.Threading;

namespace PDS_sync;

public class LockCS
{
    private static object _lock = new object();
    private static int _counter = 0;

    public static void IncrementCounter()
    {
        // Použitie lock na zabezpečenie, že iba jedno vlákno môže naraz vykonávať tento kód
        lock (_lock)
        {
            _counter++;
            Console.WriteLine($"Counter navýšený na: {_counter}");
            Thread.Sleep(500); // Simulácia práce
        }
    }

    public static void DecrementCounter()
    {
        lock (_lock)
        {
            _counter--;
            Console.WriteLine($"Counter znížený na: {_counter}");
            Thread.Sleep(500); // Simulácia práce
        }
    }
}
