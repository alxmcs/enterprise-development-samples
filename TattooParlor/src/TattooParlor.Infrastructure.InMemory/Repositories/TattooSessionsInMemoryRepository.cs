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
    public Task<TattooSession> Create(TattooSession entity)
    {
        ResolveRelations(entity);
        entity.Id = Guid.NewGuid();
        data.TattooSessions.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc/>
    public Task<bool> Delete(Guid entityId)
    {
        var res = data.TattooSessions.RemoveAll(s => s.Id == entityId);
        return Task.FromResult(res > 0);
    }

    /// <inheritdoc/>
    public Task<TattooSession?> Read(Guid entityId) => Task.FromResult(data.TattooSessions.FirstOrDefault(s => s.Id == entityId));

    /// <inheritdoc/>
    public Task<IList<TattooSession>> ReadAll() => Task.FromResult<IList<TattooSession>>([.. data.TattooSessions]);

    /// <inheritdoc/>
    public Task<IList<TattooSession>> ShowAllNonRefundable() =>
        Task.FromResult<IList<TattooSession>>([.. data.TattooSessions.Where(s => s.IsPrepaymentNonRefundable)]);

    /// <inheritdoc/>
    public Task<(decimal MinCost, decimal MaxCost, decimal AverageCost)> ShowCostRange(TattooStyle style)
    {
        var prices = data.TattooSessions.Where(s => s.Style.Style == style).Select(s => s.Price).ToArray();
        if (prices.Length == 0) return Task.FromResult((0m, 0m, 0m));
        return Task.FromResult((prices.Min(), prices.Max(), prices.Average()));
    }

    /// <inheritdoc/>
    public Task<TattooSession> Update(TattooSession entity)
    {
        var id = entity.Id;
        ResolveRelations(entity);
        Delete(id);
        Create(entity);
        entity.Id = id;
        return Task.FromResult(entity);
    }

    private void ResolveRelations(TattooSession entity)
    {
        var style = entity.Style.Style;
        var bodyZone = entity.BodyZone.BodyZone;

        entity.Customer = data.Customers.FirstOrDefault(c => c.Id == entity.CustomerId)
            ?? throw new KeyNotFoundException($"Клиент с Id {entity.CustomerId} не найден");
        entity.TattooArtist = data.TattooArtists.FirstOrDefault(a => a.Id == entity.TattooArtistId)
            ?? throw new KeyNotFoundException($"Тату-мастер с Id {entity.TattooArtistId} не найден");
        entity.Style = data.StylePricings.FirstOrDefault(s => s.Style == style)
            ?? throw new KeyNotFoundException($"Стиль татуировки {style} не найден");
        entity.BodyZone = data.BodyZonePricings.FirstOrDefault(z => z.BodyZone == bodyZone)
            ?? throw new KeyNotFoundException($"Зона тела {bodyZone} не найдена");

        entity.StyleId = entity.Style.Id;
        entity.BodyZoneId = entity.BodyZone.Id;
    }
}
