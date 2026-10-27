using Microsoft.EntityFrameworkCore;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core репозиторий для тату-сессий
/// </summary>
public class TattooSessionsEfCoreRepository(TattooParlorDbContext context) : ITattooSessionManager
{
    /// <inheritdoc/>
    public async Task<TattooSession> Create(TattooSession entity)
    {
        await ResolveRelations(entity);
        context.TattooSessions.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(Guid entityId)
        => await context.TattooSessions.Where(s => s.Id == entityId).ExecuteDeleteAsync() > 0;

    /// <inheritdoc/>
    public async Task<TattooSession?> Read(Guid entityId)
        => await context.TattooSessions.FirstOrDefaultAsync(s => s.Id == entityId);

    /// <inheritdoc/>
    public async Task<IList<TattooSession>> ReadAll()
        => await context.TattooSessions.ToListAsync();

    /// <inheritdoc/>
    public async Task<IList<TattooSession>> ShowAllNonRefundable()
    {
        var sessions = await context.TattooSessions.ToListAsync();
        return [.. sessions.Where(s => s.IsPrepaymentNonRefundable)];
    }

    /// <inheritdoc/>
    public async Task<(decimal MinCost, decimal MaxCost, decimal AverageCost)> ShowCostRange(TattooStyle style)
    {
        var prices = (await context.TattooSessions.Where(s => s.Style.Style == style).ToListAsync())
            .Select(s => s.Price)
            .ToArray();
        if (prices.Length == 0) return (0, 0, 0);
        return (prices.Min(), prices.Max(), prices.Average());
    }

    /// <inheritdoc/>
    public async Task<TattooSession> Update(TattooSession entity)
    {
        var existing = await context.TattooSessions.FirstOrDefaultAsync(s => s.Id == entity.Id)
            ?? throw new KeyNotFoundException($"Тату-сессия с Id {entity.Id} не найдена");
        await ResolveRelations(entity);

        context.Entry(existing).CurrentValues.SetValues(entity);
        existing.Customer = entity.Customer;
        existing.TattooArtist = entity.TattooArtist;
        existing.Style = entity.Style;
        existing.BodyZone = entity.BodyZone;
        await context.SaveChangesAsync();
        return existing;
    }

    private async Task ResolveRelations(TattooSession entity)
    {
        var style = entity.Style.Style;
        var bodyZone = entity.BodyZone.BodyZone;

        entity.Customer = await context.Customers.FirstOrDefaultAsync(c => c.Id == entity.CustomerId)
            ?? throw new KeyNotFoundException($"Клиент с Id {entity.CustomerId} не найден");
        entity.TattooArtist = await context.TattooArtists.FirstOrDefaultAsync(a => a.Id == entity.TattooArtistId)
            ?? throw new KeyNotFoundException($"Тату-мастер с Id {entity.TattooArtistId} не найден");
        entity.Style = await context.StylePricings.FirstOrDefaultAsync(s => s.Style == style)
            ?? throw new KeyNotFoundException($"Стиль татуировки {style} не найден");
        entity.BodyZone = await context.BodyZonePricings.FirstOrDefaultAsync(z => z.BodyZone == bodyZone)
            ?? throw new KeyNotFoundException($"Зона тела {bodyZone} не найдена");

        entity.StyleId = entity.Style.Id;
        entity.BodyZoneId = entity.BodyZone.Id;
    }
}
