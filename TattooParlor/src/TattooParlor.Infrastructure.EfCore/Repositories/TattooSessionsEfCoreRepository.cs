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
            ?? throw new KeyNotFoundException($"Tattoo session with Id {entity.Id} not found");
        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existing;
    }
}
