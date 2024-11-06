using System.Text.RegularExpressions;

namespace csharp;

// Abstraktní třída pro ukázku asynchronního zpracování
public abstract partial class AsyncAwaitDemo
{
    private static readonly HttpClient HttpClient = new();
    private static readonly IEnumerable<string> UrlList =
    [
        "https://learn.microsoft.com",
        "https://learn.microsoft.com/aspnet/core",
        "https://learn.microsoft.com/azure"
    ];

    // 1. Extrakce dat z internetu (I/O-bound příklad)
    private static async Task ExtractDataFromNetwork()
    {
        Console.WriteLine($"[Vlákno {Environment.CurrentManagedThreadId}] Začínám extrakci dat z internetu...\n");

        foreach (var url in UrlList)
        {
            var dotNetCount = await GetDotNetCount(url);
            Console.WriteLine($"[Vlákno {Environment.CurrentManagedThreadId}] {url}: počet výskytů '.NET' = {dotNetCount}");
        }
        
        Console.WriteLine($"[Vlákno {Environment.CurrentManagedThreadId}] Extrakce dat z internetu dokončena.\n");
    }

    // Metoda pro získání počtu výskytů ".NET" na daném URL
    private static async Task<int> GetDotNetCount(string url)
    {
        Console.WriteLine($"[Vlákno {Environment.CurrentManagedThreadId}] Stahuji HTML obsah z: {url}");
        var html = await HttpClient.GetStringAsync(url);
        var count = MyRegex().Matches(html).Count;
        return count;
    }

    // 2. Čekání na dokončení více úloh (I/O-bound příklad - získání dat uživatelů)
    private static async Task WaitForMultipleTasks()
    {
        var userIds = new[] { 1, 2, 3, 4 };
        var users = await GetUsersAsync(userIds);

        foreach (var user in users)
        {
            Console.WriteLine($"[Vlákno {Environment.CurrentManagedThreadId}] Uživatel {user.Id}: Aktivní = {user.IsEnabled}");
        }

        Console.WriteLine($"[Vlákno {Environment.CurrentManagedThreadId}] Načítání uživatelů dokončeno.\n");
    }

    // Metoda pro získání seznamu uživatelů asynchronně
    private static async Task<IEnumerable<User>> GetUsersAsync(IEnumerable<int> userIds)
    {
        Console.WriteLine($"[Vlákno {Environment.CurrentManagedThreadId}] Načítám seznam uživatelů...");

        var getUserTasks = userIds.Select(GetUserAsync);
        var users = await Task.WhenAll(getUserTasks);
        
        Console.WriteLine($"[Vlákno {Environment.CurrentManagedThreadId}] Všichni uživatelé byli načteni.");
        return users;
    }

    // Metoda pro simulaci načítání uživatele
    private static async Task<User> GetUserAsync(int userId)
    {
        await Task.Delay(100); // Simulace zpoždění
        var user = new User { Id = userId, IsEnabled = userId % 2 == 0 };
        Console.WriteLine($"[Vlákno {Environment.CurrentManagedThreadId}] Uživatel {user.Id} načten.");
        return user;
    }

    // 3. Simulace výpočetně náročné operace
    private static async Task PerformAsyncDamageCalculation()
    {
        Console.WriteLine($"[Vlákno {Environment.CurrentManagedThreadId}] Začínám výpočet poškození...");
        
        var result = await Task.Run(() =>
        {
            Task.Delay(200).Wait(); // Simulace náročného výpočtu
            return new Random().Next(50, 100);
        });
        
        Console.WriteLine($"[Vlákno {Environment.CurrentManagedThreadId}] Výsledek poškození = {result}");
    }

    // Hlavní metoda pro spuštění ukázky
    public static async Task RunAsyncAwaitDemo()
    {
        Console.WriteLine($"[Vlákno {Environment.CurrentManagedThreadId}] Spouštím aplikaci...");

        await ExtractDataFromNetwork();         // Scénář 1: I/O-bound extrakce z internetu
        await WaitForMultipleTasks();           // Scénář 2: Čekání na dokončení více úloh 
        await PerformAsyncDamageCalculation();  // Scénář 3: CPU-bound výpočet

        Console.WriteLine($"[Vlákno {Environment.CurrentManagedThreadId}] Aplikace ukončena.");
    }

    [GeneratedRegex(@"\.NET")]
    private static partial Regex MyRegex();
}