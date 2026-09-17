using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Model.References;

namespace TattooParlor.Infrastructure.InMemory.Repositories;

/// <summary>
/// Ин-мемори репозиторий для справочника стилей
/// </summary>
public class StylePricingInMemoryRepository : ICrudRepository<StylePricing, int>
{
    /// <inheritdoc/> 
    public StylePricing Create(StylePricing entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/> 
    public bool Delete(int entityId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/> 
    public StylePricing? Read(int entityId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/> 
    public IList<StylePricing> ReadAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/> 
    public StylePricing Update(StylePricing entity)
    {
        throw new NotImplementedException();
    }
}
