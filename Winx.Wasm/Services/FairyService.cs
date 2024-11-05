using Winx.Wasm.Domain;

namespace Winx.Wasm.Services;

public class FairyService
{
    private readonly List<Fairy> _fairies = DataSeeder.Seed();

    public Fairy GetRandomFairy()
    {
        var rand = new Random();
        return _fairies.ElementAt(rand.Next(_fairies.Count));
    }
}
