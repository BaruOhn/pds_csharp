namespace delegati
{
    public class DelegatiImplementace
    {
        // Definice jednoduchého delegáta
        public delegate void SimpleDelegate(string message);

        // Metody pro jednoduchého delegáta
        public void ShowMessage(string message)
        {
            Console.WriteLine($"První zpráva: {message}");
        }

        public void ShowMessageUpperCase(string message)
        {
            Console.WriteLine($"Zpráva velkými písmeny: {message.ToUpper()}");
        }
        
        // Metoda pro použití jednoduchého delegáta
        public void UseSimpleDelegate()
        {
            SimpleDelegate simpleDelegate = ShowMessage;
            Console.WriteLine("1. Původní delegát:");
            simpleDelegate("Hello!");
            
            Console.WriteLine("\n2. Změna reference delegáta:");
            simpleDelegate = ShowMessageUpperCase;
            simpleDelegate("Hello!");
        }
        
        
        
        // Definice multicast delegáta
        public delegate void MultiDelegate(string message);

        // Metoda pro multicast delegáta
        public void ShowGreeting(string message)
        {
            Console.WriteLine($"Druhá zpráva: {message}");
        }

        // Metoda pro použití multicast delegáta
        public void UseMultiDelegate()
        {
            MultiDelegate multiDelegate = ShowMessage;
            Console.WriteLine("1. První metoda:");
            multiDelegate("Hello!");
            
            Console.WriteLine("\n2. Přidání druhé metody (+=):");
            multiDelegate += ShowGreeting;
            multiDelegate("Hello!");
            
            Console.WriteLine("\n3. Odebrání první metody (-):");
            multiDelegate -= ShowMessage;
            multiDelegate("Hello!");
        }
        
        
        
        // Asynchronní volání metod
        public async Task UseAsyncDelegate()
        {
            // Definujeme různé způsoby zpracování zprávy
            SimpleDelegate zpracovaniA = message =>
            {
                Console.WriteLine($"Způsob A: Začínám zpracovávat zprávu: {message}");
                Thread.Sleep(2000); // Simulace dlouhé operace
                Console.WriteLine($"Způsob A: Zpráva zpracována");
            };

            SimpleDelegate zpracovaniB = message =>
            {
                Console.WriteLine($"Způsob B: Převádím zprávu na velká písmena: {message.ToUpper()}");
                Thread.Sleep(1000); // Kratší operace
                Console.WriteLine($"Způsob B: Zpráva zpracována");
            };

            Console.WriteLine("1. Hlavní vlákno: Spouštím asynchronní operaci se způsobem A");
            var taskA = Task.Run(() => zpracovaniA("Hello World"));
    
            Console.WriteLine("2. Hlavní vlákno: Mezitím můžu dělat další věci");
            for(int i = 1; i <= 3; i++)
            {
                Console.WriteLine($"Hlavní vlákno: Pracuji... ({i}/3)");
                Thread.Sleep(500);
            }
    
            await taskA;
    
            Console.WriteLine("\n3. Hlavní vlákno: Nyní spustím způsob B");
            var taskB = Task.Run(() => zpracovaniB("Hello World"));
            await taskB;
    
            Console.WriteLine("4. Hlavní vlákno: Vše dokončeno");
        }

        
        
        // Multicast delegáti pro paralelní volání více metod
        public void UseMultiDelegateInParallel()
        {
            MultiDelegate multiDelegate = ShowMessage;
            multiDelegate += ShowGreeting;

            Console.WriteLine("Spouštím paralelní volání...");
            var task = Task.Run(() => 
            {
                Console.WriteLine("Paralelní exekuce začíná");
                multiDelegate("Hello from parallel multicast delegate!");
                Console.WriteLine("Paralelní exekuce končí");
            });
            
            Console.WriteLine("Hlavní vlákno pokračuje...");
            task.Wait(); // Počkáme na dokončení
        }

        
        
        // Definice delegáta pro callback
        public delegate void OperationCompleted(string result);

        // Metoda demonstrující použití callbacku
        public void StartOperation(OperationCompleted callback)
        {
            Console.WriteLine("Začínám dlouhou operaci...");
    
            // Spustíme dlouhou operaci na pozadí
            Task.Run(() =>
            {
                Console.WriteLine("Operace probíhá na pozadí...");
                Thread.Sleep(2000);
                callback("Operace úspěšně dokončena!");    
                Thread.Sleep(500);
                callback("Provádím dodatečné operace...");
                Thread.Sleep(500);
                callback("Vše hotovo!");
            });

            // Mezitím děláme něco jiného v hlavním vlákně
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Hlavní program: Provádím další práci... ({i}/5)");
                Thread.Sleep(700); // Simulujeme nějakou práci
            }
        }
    }
}