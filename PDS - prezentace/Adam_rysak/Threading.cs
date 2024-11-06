public class ThreadingExamples
{
    public static void RunThreadingExamples()
    {
        Console.WriteLine("Hlavní vlákno spuštěno.");

        // 1. Ukázka manuálního vytvoření a spuštění vlákna
        Thread thread1 = new Thread(RunInThread);
        thread1.Start("Vlákno 1");

        // 2. Ukázka Parallel.For pro paralelizaci úkolů
        Parallel.For(0, 5, i =>
        {
            Console.WriteLine($"Parallel.For iterace {i} zpracována na vlákně {Thread.CurrentThread.ManagedThreadId}");
        });

        // 3. Ukázka ThreadPool pro zařazení úlohy do poolu vláken
        ThreadPool.QueueUserWorkItem(ThreadPoolTask, "Úloha v ThreadPoolu");

        // 4. Ukázka výjimky ve vlákně
        Thread thread2 = new Thread(() =>
        {
            try
            {
                throw new InvalidOperationException("Simulovaná výjimka ve vlákně.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Výjimka zachycena ve vlákně: {ex.Message}");
            }
        });
        thread2.Start();

       

        // Počkejte, dokud všechna vlákna neskončí
        thread1.Join();
        thread2.Join();

        Console.WriteLine("Hlavní vlákno dokončeno.");
    }

    // Metoda pro samostatné vlákno (používá Thread)
    private static void RunInThread(object threadName)
    {
        Console.WriteLine($"{threadName} spuštěno na vlákně {Thread.CurrentThread.ManagedThreadId}.");
        Thread.Sleep(500); // Simulace nějaké činnosti
        Console.WriteLine($"{threadName} dokončeno.");
    }

    // Metoda pro ThreadPool úlohu
    private static void ThreadPoolTask(object state)
    {
        Console.WriteLine($"{state} spuštěna na vlákně {Thread.CurrentThread.ManagedThreadId}.");
        Thread.Sleep(300); // Simulace nějaké činnosti
        Console.WriteLine($"{state} dokončena.");
    }
}