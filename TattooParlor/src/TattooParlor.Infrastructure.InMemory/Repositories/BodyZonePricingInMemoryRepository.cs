using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Model.References;

namespace TattooParlor.Infrastructure.InMemory.Repositories;

/// <summary>
/// Ин-мемори репозиторий для справочника частей тела
/// </summary>
public class BodyZonePricingInMemoryRepository : ICrudRepository<BodyZonePricing, int>
{
    /// <inheritdoc/>   
    public BodyZonePricing Create(BodyZonePricing entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/> 
    public bool Delete(int entityId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/> 
    public BodyZonePricing? Read(int entityId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/> 
    public IList<BodyZonePricing> ReadAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/> 
    public BodyZonePricing Update(BodyZonePricing entity)
    {
        throw new NotImplementedException();
    }
}
