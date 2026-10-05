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
        entity.Id = Guid.NewGuid();
        data.TattooSessions.Add(entity);
        return entity;
    }

    /// <inheritdoc/> 
    public bool Delete(Guid entityId)
    {
        var res = data.TattooSessions.RemoveAll(s => s.Id == entityId);
        return res > 0;
    }

    /// <inheritdoc/> 
    public TattooSession? Read(Guid entityId) => data.TattooSessions.FirstOrDefault(s => s.Id == entityId);

    /// <inheritdoc/> 
    public IList<TattooSession> ReadAll() => [.. data.TattooSessions];

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
        Delete(entity.Id);
        Create(entity);
        return entity;
    }
}
