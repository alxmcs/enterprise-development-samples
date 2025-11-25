using Winx.Wasm.Domain;

namespace Winx.Wasm.Services;

/// <summary>
/// Служба для случайного получения феи винкс из коллекции фей винкс
/// </summary>
public class FairyService
{
    private readonly List<Fairy> _fairies = DataSeeder.Seed();

    /// <summary>
    /// Метод получения случайной феи винкс из коллекции фей винкс
    /// </summary>
    /// <returns>Случайная фея винкс</returns>
    public Fairy GetRandomFairy()
    {
        var rand = new Random();
        return _fairies.ElementAt(rand.Next(_fairies.Count));
    }
}
