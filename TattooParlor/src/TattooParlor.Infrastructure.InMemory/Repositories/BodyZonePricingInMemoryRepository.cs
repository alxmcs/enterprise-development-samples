using TattooParlor.Domain.Data;
using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Model.References;

namespace TattooParlor.Infrastructure.InMemory.Repositories;

/// <summary>
/// Ин-мемори репозиторий для справочника частей тела
/// </summary>
public class BodyZonePricingInMemoryRepository(DataSeeder data) : ICrudRepository<BodyZonePricing, int>
{
    /// <inheritdoc/>   
    public BodyZonePricing Create(BodyZonePricing entity)
    {
entity.Id = data.BodyZonePricings.Count == 0 ? 1 : data.BodyZonePricings.Max(b => b.Id) + 1;
        data.BodyZonePricings.Add(entity);
        return entity;
    }

    /// <inheritdoc/> 
    public bool Delete(int entityId)
    {
        var res = data.BodyZonePricings.RemoveAll(s => s.Id == entityId);
        return res > 0;
    }

    /// <inheritdoc/> 
    public BodyZonePricing? Read(int entityId) => data.BodyZonePricings.FirstOrDefault(s => s.Id == entityId);

    /// <inheritdoc/> 
    public IList<BodyZonePricing> ReadAll() => [.. data.BodyZonePricings];

    /// <inheritdoc/> 
    public BodyZonePricing Update(BodyZonePricing entity)
    {
        Delete(entity.Id);
        Create(entity);
        return entity;
    }
}
