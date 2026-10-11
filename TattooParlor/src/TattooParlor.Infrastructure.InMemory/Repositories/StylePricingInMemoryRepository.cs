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
    public StylePricing Create(StylePricing entity)
    {
entity.Id = data.StylePricings.Count == 0 ? 1 : data.StylePricings.Max(s => s.Id) + 1;
        data.StylePricings.Add(entity);
        return entity;
    }

    /// <inheritdoc/> 
    public bool Delete(int entityId)
    {
        var res = data.StylePricings.RemoveAll(s => s.Id == entityId);
        return res > 0;
    }

    /// <inheritdoc/> 
    public StylePricing? Read(int entityId) => data.StylePricings.FirstOrDefault(s => s.Id == entityId);

    /// <inheritdoc/> 
    public IList<StylePricing> ReadAll() => [.. data.StylePricings];

    /// <inheritdoc/> 
    public StylePricing Update(StylePricing entity)
    {
        Delete(entity.Id);
        Create(entity);
        return entity;
    }
}
