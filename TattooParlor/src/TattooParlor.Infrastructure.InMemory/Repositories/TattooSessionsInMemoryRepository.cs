using TattooParlor.Domain.Data;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Infrastructure.InMemory.Repositories;

/// <summary>
/// Ин-мемори репозиторий для тату-сессий
/// </summary>
public class TattooSessionsInMemoryRepository(DataSeeder data) : ITattooSessionManager
{
    /// <inheritdoc/> 
    public TattooSession Create(TattooSession entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/> 
    public bool Delete(Guid entityId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/> 
    public TattooSession? Read(Guid entityId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/> 
    public IList<TattooSession> ReadAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/> 
    public IList<TattooSession> ShowAllNonRefundable() =>
        [.. data.TattooSessions.Where(s => s.IsPrepaymentNonRefundable)];

    /// <inheritdoc/> 
    public (decimal MinCost, decimal MaxCost, decimal AverageCost) ShowCostRange(TattooStyle style)
    {
        var prices = data.TattooSessions.Where(s => s.Style.Style == style).Select(s => s.Price).ToArray();
        if (prices.Length == 0) return (0, 0, 0);
        return (prices.Min(), prices.Max(), prices.Average());
    }

    /// <inheritdoc/> 
    public TattooSession Update(TattooSession entity)
    {
        throw new NotImplementedException();
    }
}
