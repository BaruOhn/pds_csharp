namespace delegati
{
    public class DelegatiRunner
    {
        public static void RunDelegati()
        {
            Console.WriteLine("= = = = = = = = = = = = = = = = = = =");
            Console.WriteLine("|         DELEGÁTY V C#             |");
            Console.WriteLine("= = = = = = = = = = = = = = = = = = =");
            Console.WriteLine("Vyberte, kterou ukázku chcete spustit:");
            Console.WriteLine("= = = = = = = = = = = = = = = = = = =");
            Console.WriteLine("| 1 - Simple Delegate               |");
            Console.WriteLine("| 2 - Multicast Delegate           |");
            Console.WriteLine("| 3 - Async Delegate               |");
            Console.WriteLine("| 4 - Parallel Multicast Delegate  |");
            Console.WriteLine("| 5 - Callback Demo                |");
            Console.WriteLine("= = = = = = = = = = = = = = = = = = =");
            Console.Write("Zadejte číslo: ");

            string input = Console.ReadLine();
            var implementace = new DelegatiImplementace();

            switch (input)
            {
                case "1":
                    Console.WriteLine("\nSimple Delegate\n");
                    Console.WriteLine("Ukázka jednoduchého delegáta, který může odkazovat na různé metody se stejnou signaturou.\n");
                    implementace.UseSimpleDelegate();
                    break;

                case "2":
                    Console.WriteLine("\nMulticast Delegate\n");
                    Console.WriteLine("Ukázka multicast delegáta, který může volat více metod najednou.\n");
                    implementace.UseMultiDelegate();
                    break;

                case "3":
                    Console.WriteLine("\nAsync Delegate\n");
                    Console.WriteLine("Ukázka asynchronního volání delegáta.\n");
                    implementace.UseAsyncDelegate().Wait();
                    break;

                case "4":
                    Console.WriteLine("\nParallel Multicast Delegate\n");
                    Console.WriteLine("Ukázka paralelního volání multicast delegáta.\n");
                    implementace.UseMultiDelegateInParallel();
                    Thread.Sleep(2000); // Počkáme na dokončení paralelního volání
                    break;

                case "5":
                    Console.WriteLine("\nCallback Demo\n");
                    Console.WriteLine("Ukázka použití delegáta jako callback funkce.");
                    Console.WriteLine("Všimněte si, že hlavní program pokračuje v práci, zatímco čeká na callback.\n");
    
                    implementace.StartOperation(result => 
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"CALLBACK: {result}");
                        Console.ResetColor();
                    });
    
                    Thread.Sleep(4000); // Počkáme, až se dokončí všechny operace
                    break;
            }
        }
    }
}