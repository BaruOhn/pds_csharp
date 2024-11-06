using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDS_sync
{
   public class MasterPrimitives
    {
        public static void RunPrimitives()
        {
            Console.WriteLine("= = = = = = = = = = = = = = = = = = =");
            Console.WriteLine("|   SYNCHRONIZAČNÉ PRIMITÍVA V C#   |");
            Console.WriteLine("= = = = = = = = = = = = = = = = = = =");
            Console.WriteLine("Vyberte, ktorú ukážku chcete spustiť:");
            Console.WriteLine("= = = = = = = = = = = = = = = = = = =");
            Console.WriteLine("| 1 - Lock (jednoduchý zámok)       |");
            Console.WriteLine("| 2 - Monitor                       |");
            Console.WriteLine("| 3 - Semaphore                     |");
            Console.WriteLine("| 4 - Mutex                         |");
            Console.WriteLine("| 5 - ReaderWriterLockSlim          |");
            Console.WriteLine("= = = = = = = = = = = = = = = = = = =");
            Console.Write("Zadajte číslo: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    RunLockExample();
                    break;

                case "2":
                    RunMonitorExample();
                    break;

                case "3":
                    RunSemaphoreExample();
                    break;

                case "4":
                    RunMutexExample();
                    break;

                case "5":
                    RunRWLSExample();
                    break;

                default:
                    Console.WriteLine("Neplatná voľba.");
                    break;
            }
        }

        public static void RunLockExample()
        {
            Console.WriteLine("\nLock (jednoduchý zámok)\n");
            Console.WriteLine("Vytvorili a spustili sme 9 vlákien, ktoré postupne navýšia counter na 5 a potom ho znížia na 1.\n");

            Thread thread1 = new Thread(LockCS.IncrementCounter);
            Thread thread2 = new Thread(LockCS.IncrementCounter);
            Thread thread3 = new Thread(LockCS.IncrementCounter);
            Thread thread4 = new Thread(LockCS.IncrementCounter);
            Thread thread5 = new Thread(LockCS.IncrementCounter);
            Thread thread6 = new Thread(LockCS.DecrementCounter);
            Thread thread7 = new Thread(LockCS.DecrementCounter);
            Thread thread8 = new Thread(LockCS.DecrementCounter);
            Thread thread9 = new Thread(LockCS.DecrementCounter);

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();
            thread6.Start();
            thread7.Start();
            thread8.Start();
            thread9.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();
            thread5.Join();
            thread6.Join();
            thread7.Join();
            thread8.Join();
            thread9.Join();
        }

        public static void RunMonitorExample()
        {
            Console.WriteLine("\nMonitor\n");
            Console.WriteLine("Máme 2 vlákna:");
            Console.WriteLine("- waiter: čaká na splnenie podmienky, spustené ako prvé");
            Console.WriteLine("- signaler: signalizuje zmenu podmienky, spustené ako druhé\n");

            Thread waiter = new Thread(MonitorCS.WaitForCondition);
            Thread signaler = new Thread(MonitorCS.SignalCondition);

            waiter.Start();
            Thread.Sleep(1000); // pauza medzi spustením waitera a signalera 
            signaler.Start();

            waiter.Join();
            signaler.Join();
        }

        public static void RunSemaphoreExample()
        {
            Console.WriteLine("\nSemaphore\n");
            Console.WriteLine("Vytvorili a spustili sme 3 vlákna, ale iba 2 môžu naraz vstúpiť do KS.");
            Console.WriteLine("Semaphore(2, 2)\n");

            Thread thread1 = new Thread(SemaphoreCS.AccessLimitedResource) { Name = "Vlákno 1" };
            Thread thread2 = new Thread(SemaphoreCS.AccessLimitedResource) { Name = "Vlákno 2" };
            Thread thread3 = new Thread(SemaphoreCS.AccessLimitedResource) { Name = "Vlákno 3" };

            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
        }

        public static void RunMutexExample()
        {
            Console.WriteLine("\nMutex\n");
            Console.WriteLine("Vytvorili a spustili sme 3 vlákna, ktoré čakajú na získanie Mutexu, ale naraz môže Mutex držať len jedno vlákno.");
            Console.WriteLine("Mutex(false, \"PDS_Sync_Mutex\")\n");

            Thread thread1 = new Thread(MutexCS.AccessSharedResource) { Name = "Vlákno 1" };
            Thread thread2 = new Thread(MutexCS.AccessSharedResource) { Name = "Vlákno 2" };
            Thread thread3 = new Thread(MutexCS.AccessSharedResource) { Name = "Vlákno 3" };

            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
        }

        public static void RunRWLSExample()
        {
            Console.WriteLine("\nReaderWriterLockSlim\n");
            Console.WriteLine("Vytvorili a spustili sme 3 vlákna na čítanie a 2 vlákna na zápis, aby sme demonštrovali, ako viacerí čitatelia môžu čítať súčasne, ale iba jeden zapisovateľ môže zapisovať.\n");

            Thread reader1 = new Thread(ReaderWriterLockSlimCS.ReadData) { Name = "Čitateľ 1" };
            Thread reader2 = new Thread(ReaderWriterLockSlimCS.ReadData) { Name = "Čitateľ 2" };
            Thread reader3 = new Thread(ReaderWriterLockSlimCS.ReadData) { Name = "Čitateľ 3" };
            Thread writer1 = new Thread(ReaderWriterLockSlimCS.WriteData) { Name = "Zapisovateľ 1" };
            Thread writer2 = new Thread(ReaderWriterLockSlimCS.WriteData) { Name = "Zapisovateľ 2" };

            reader1.Start();
            reader2.Start();
            reader3.Start();
            writer1.Start();
            writer2.Start();
        }
    }
}
