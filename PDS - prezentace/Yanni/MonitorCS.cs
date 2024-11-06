using System;
using System.Threading;

namespace PDS_sync;

public class MonitorCS
{
    private static object _lock = new object();
    private static bool _conditionMet = false;

    private static int sharedResource = 0;

    public static void AccessResource()
    {
        Monitor.Enter(_lock);
        try
        {
            // Kritická sekcia
            sharedResource++;
            Console.WriteLine($"Resource value: {sharedResource}");
        }
        finally
        {
            Monitor.Exit(_lock);
        }
    }

    public static void WaitForCondition()   // waiter
    {
        lock (_lock)
        {
            Console.WriteLine("Waiter: \t\"Čakám na splnenie podmienky, aby som mohol pokračovať.\"");
            while (!_conditionMet)
            {
                // Monitor.Wait: Uvoľní zámok a čaká, kým nedostane signál
                Monitor.Wait(_lock);
            }
            Console.WriteLine("Waiter: \t\"Podmienka splnená! Pokračujem vo svojej činnosti...\"");
        }
    }

    public static void SignalCondition()    // signaler
    {
        lock (_lock)
        {
            Console.WriteLine("Signaler:\t\"Signalizujem, že jedno vlákno môže pokračovať v činnosti, podmienka bola zmenená.\"");
            _conditionMet = true;
            // Monitor.Pulse: Posiela signál jednému čakajúcemu vláknu
            Monitor.Pulse(_lock);
        }
    }

    public static void SignalAllConditions()
    {
        lock (_lock)
        {
            Console.WriteLine("Signaler:\t\"Signalizujem, že všetky vlákna môžu pokračovať v činnosti, podmienka bola zmenená.\"");
            _conditionMet = true;
            // Monitor.PulseAll: Posiela signál všetkým čakajúcim vláknam
            Monitor.PulseAll(_lock);
        }
    }
}
