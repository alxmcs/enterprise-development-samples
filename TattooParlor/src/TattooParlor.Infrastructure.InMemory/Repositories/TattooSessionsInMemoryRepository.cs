using TattooParlor.Domain.Data;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Infrastructure.InMemory.Repositories;

public class TattooSessionsInMemoryRepository(DataSeeder data) : ITattooSessionManager
{
    public TattooSession Create(TattooSession entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Guid entityId)
    {
        throw new NotImplementedException();
    }

    public TattooSession? Read(Guid entityId)
    {
        throw new NotImplementedException();
    }

    public IList<TattooSession> ReadAll()
    {
        throw new NotImplementedException();
    }

    public IList<TattooSession> ShowAllNonRefundable() =>
        [.. data.TattooSessions.Where(s => s.IsPrepaymentNonRefundable)];

    public (decimal MinCost, decimal MaxCost, decimal AverageCost) ShowCostRange(TattooStyle style)
    {
        var prices = data.TattooSessions.Where(s => s.Style.Style == style).Select(s => s.Price).ToArray();
        if (prices.Length == 0) return (0, 0, 0);
        return (prices.Min(), prices.Max(), prices.Average());
    }


    public TattooSession Update(TattooSession entity)
    {
        throw new NotImplementedException();
    }
}
