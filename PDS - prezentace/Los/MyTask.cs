using System.Diagnostics;

namespace PDS___prezentace.Los
{
    class MyTask
    {
        static void DoWork()
        {
            // Simulace krátké operace/čekání na data
            Thread.Sleep(10); // Čeká 10 ms
        }

        static void RunThreads(int numberOfThreads)
        {
            Thread[] threads = new Thread[numberOfThreads];


            // Spuštění vláken
            for (int i = 0; i < numberOfThreads; i++)
            {
                threads[i] = new Thread(DoWork);
                threads[i].Start();
            }

            // Čekání na dokončení všech vláken
            for (int i = 0; i < numberOfThreads; i++)
            {
                threads[i].Join();
            }
        }

        static void RunTasks(int numberOfTasks)
        {
            Task[] tasks = new Task[numberOfTasks];


            // Spuštění Tasků
            for (int i = 0; i < numberOfTasks; i++)
            {
                tasks[i] = Task.Run(() => DoWork());
            }

            // Čekání na dokončení všech Tasků
            Task.WaitAll(tasks);

        }

        public static void RunExample()
        {
            Stopwatch sw = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();

            int count = 1000;

            sw.Start();
            RunThreads(count);
            sw.Stop();


            sw2.Start();
            RunTasks(count);
            sw2.Stop();

            Console.WriteLine("Total time using Threads: {0} ms", sw.ElapsedMilliseconds);
            Console.WriteLine("Total time using Tasks: {0} ms", sw2.ElapsedMilliseconds);


        }
    }
}
