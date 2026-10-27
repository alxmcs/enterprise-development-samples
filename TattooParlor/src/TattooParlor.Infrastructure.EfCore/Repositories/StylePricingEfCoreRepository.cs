using Microsoft.EntityFrameworkCore;
using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Model.References;

namespace TattooParlor.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core репозиторий для справочника стилей
/// </summary>
public class StylePricingEfCoreRepository(TattooParlorDbContext context) : ICrudRepository<StylePricing, int>
{
    /// <inheritdoc/>
    public async Task<StylePricing> Create(StylePricing entity)
    {
        context.StylePricings.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int entityId)
        => await context.StylePricings.Where(s => s.Id == entityId).ExecuteDeleteAsync() > 0;

    /// <inheritdoc/>
    public async Task<StylePricing?> Read(int entityId)
        => await context.StylePricings.FirstOrDefaultAsync(s => s.Id == entityId);

    /// <inheritdoc/>
    public async Task<IList<StylePricing>> ReadAll()
        => await context.StylePricings.ToListAsync();

    /// <inheritdoc/>
    public async Task<StylePricing> Update(StylePricing entity)
    {
        var existing = await context.StylePricings.FirstOrDefaultAsync(s => s.Id == entity.Id)
            ?? throw new KeyNotFoundException($"Style pricing with Id {entity.Id} not found");
        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existing;
    }
}
