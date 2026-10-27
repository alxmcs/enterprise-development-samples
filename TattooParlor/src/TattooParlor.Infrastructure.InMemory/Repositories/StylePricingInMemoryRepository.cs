using TattooParlor.Domain.Data;
using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Model.References;

namespace TattooParlor.Infrastructure.InMemory.Repositories;

/// <summary>
/// Ин-мемори репозиторий для справочника стилей
/// </summary>
public class StylePricingInMemoryRepository(DataSeeder data) : ICrudRepository<StylePricing, int>
{
    /// <inheritdoc/>
    public Task<StylePricing> Create(StylePricing entity)
    {
        entity.Id = data.StylePricings.Count == 0 ? 1 : data.StylePricings.Max(s => s.Id) + 1;
        data.StylePricings.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc/>
    public Task<bool> Delete(int entityId)
    {
        var res = data.StylePricings.RemoveAll(s => s.Id == entityId);
        return Task.FromResult(res > 0);
    }

    /// <inheritdoc/>
    public Task<StylePricing?> Read(int entityId) => Task.FromResult(data.StylePricings.FirstOrDefault(s => s.Id == entityId));

    /// <inheritdoc/>
    public Task<IList<StylePricing>> ReadAll() => Task.FromResult<IList<StylePricing>>([.. data.StylePricings]);

    /// <inheritdoc/>
    public Task<StylePricing> Update(StylePricing entity)
    {
        Delete(entity.Id);
        Create(entity);
        return Task.FromResult(entity);
    }
}
