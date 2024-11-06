using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDS_sync
{
    public class MutexCS
    {
        // Vytvorenie pomenovaného Mutexu, ktorý môžu zdieľať rôzne procesy
        private static Mutex mutex = new Mutex(false, "PDS_Sync_Mutex");
        private static int _counter = 0;

        public static void AccessSharedResource()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}:\t \"Čakám na získanie Mutexu.\"");

            // WaitOne: Získanie zámku Mutex
            mutex.WaitOne();
            try
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}:\t \"Získal som Mutex.\"");
                _counter++;
                Console.WriteLine($"{Thread.CurrentThread.Name}:\t \"Navyšujem counter na: {_counter}\"");
            }
            finally
            {
                // ReleaseMutex: Uvoľnenie zámku
                mutex.ReleaseMutex();
                Console.WriteLine($"{Thread.CurrentThread.Name}:\t \"Uvoľňujem Mutex.\"");
            }
        }
    }
}
