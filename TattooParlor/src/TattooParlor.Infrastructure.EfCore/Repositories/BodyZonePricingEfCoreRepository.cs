using Microsoft.EntityFrameworkCore;
using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Model.References;

namespace TattooParlor.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core репозиторий для справочника частей тела
/// </summary>
public class BodyZonePricingEfCoreRepository(TattooParlorDbContext context) : ICrudRepository<BodyZonePricing, int>
{
    /// <inheritdoc/>
    public async Task<BodyZonePricing> Create(BodyZonePricing entity)
    {
        context.BodyZonePricings.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int entityId)
        => await context.BodyZonePricings.Where(b => b.Id == entityId).ExecuteDeleteAsync() > 0;

    /// <inheritdoc/>
    public async Task<BodyZonePricing?> Read(int entityId)
        => await context.BodyZonePricings.FirstOrDefaultAsync(b => b.Id == entityId);

    /// <inheritdoc/>
    public async Task<IList<BodyZonePricing>> ReadAll()
        => await context.BodyZonePricings.ToListAsync();

    /// <inheritdoc/>
    public async Task<BodyZonePricing> Update(BodyZonePricing entity)
    {
        var existing = await context.BodyZonePricings.FirstOrDefaultAsync(b => b.Id == entity.Id)
            ?? throw new KeyNotFoundException($"Body zone pricing with Id {entity.Id} not found");
        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existing;
    }
}
