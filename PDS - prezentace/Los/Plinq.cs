using System.Diagnostics;

namespace PDS___prezentace.Los
{
    class Plinq
    {
        public static List<int> GenerateNumbers(int n, int min, int max)
        {
            List<int> numbers = new List<int>(n);
            Random random = new Random();

            for (int i = 0; i < n; i++)
            {
                int number = random.Next(min, max + 1);
                numbers.Add(number);
            }

            return numbers;
        }

        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 2; i <= boundary; ++i)
                if (number % i == 0)
                    return false;

            return true;
        }


        public static void RunExample()
        {
            // Generování čísel
            List<int> generatedNumbers = GenerateNumbers(10_000_000, 1, 10_000_000);

            // Měření pro x % 2 == 0
            Stopwatch stopwatch1 = Stopwatch.StartNew();
            var seq = generatedNumbers.Where(x => x % 2 == 0).ToList();
            stopwatch1.Stop();

            Stopwatch stopwatch2 = Stopwatch.StartNew();
            var par = generatedNumbers.AsParallel().Where(x => x % 2 == 0).ToList();
            stopwatch2.Stop();

            Console.WriteLine($"Sequential processing (x % 2 == 0) took: {stopwatch1.ElapsedMilliseconds} ms");
            Console.WriteLine($"Parallel processing (x % 2 == 0) took: {stopwatch2.ElapsedMilliseconds} ms");

            // Měření pro IsPrime(x)
            stopwatch1.Restart();
            var seq1 = generatedNumbers.Where(x => IsPrime(x)).ToList();
            stopwatch1.Stop();

            stopwatch2.Restart();
            var par1 = generatedNumbers.AsParallel().Where(x => IsPrime(x)).ToList();
            stopwatch2.Stop();

            Console.WriteLine($"Sequential processing (IsPrime) took: {stopwatch1.ElapsedMilliseconds} ms");
            Console.WriteLine($"Parallel processing (IsPrime) took: {stopwatch2.ElapsedMilliseconds} ms");

        }
    }
}
