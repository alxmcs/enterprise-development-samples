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

    public (decimal MinCost, decimal MaxCost, decimal AverageCost) ShowCostRange(TattooStyle style) =>
        data.TattooSessions
            .Where(s => s.Style.Style == style)
            .Aggregate((MinCost: decimal.MaxValue, MaxCost: decimal.MinValue, AverageCost: 0m), (acc, s) =>
            {
                acc.MinCost = Math.Min(acc.MinCost, s.Price);
                acc.MaxCost = Math.Max(acc.MaxCost, s.Price);
                acc.AverageCost += s.Price;
                return acc;
            }, acc =>
            {
                var count = data.TattooSessions.Count(s => s.Style.Style == style);
                if (count == 0) return (0, 0, 0);
                acc.AverageCost /= count;
                return acc;
            });


    public TattooSession Update(TattooSession entity)
    {
        throw new NotImplementedException();
    }
}
