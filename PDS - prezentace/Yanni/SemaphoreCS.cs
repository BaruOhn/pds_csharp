using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDS_sync
{
    public class SemaphoreCS
    {
        // Inicializácia semaforu
        private static Semaphore semaphore = new Semaphore(2, 2);
        private static int _counter = 0;

        public static void AccessLimitedResource()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}:\t \"Skúšam získať povolenie na prístup do KS.\"");

            // Získanie povolenia
            semaphore.WaitOne();
            try
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}:\t \"Vstúpil som do KS.\"");
                _counter++;
                Console.WriteLine($"{Thread.CurrentThread.Name}:\t \"Navyšujem counter na: {_counter}");
                Console.WriteLine($"{Thread.CurrentThread.Name}:\t \"Odchádzam z KS.\"");
            }
            finally
            {
                // Uvoľnenie povolenia
                Console.WriteLine($"{Thread.CurrentThread.Name}:\t \"Uvoľňujem za sebou semafor.\"");
                semaphore.Release();
            }
        }
    }
}
