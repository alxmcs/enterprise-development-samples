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
    public Task<BodyZonePricing> Create(BodyZonePricing entity)
    {
        entity.Id = data.BodyZonePricings.Count == 0 ? 1 : data.BodyZonePricings.Max(b => b.Id) + 1;
        data.BodyZonePricings.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc/> 
    public Task<bool> Delete(int entityId)
    {
        var res = data.BodyZonePricings.RemoveAll(s => s.Id == entityId);
        return Task.FromResult(res > 0);
    }

    /// <inheritdoc/> 
    public Task<BodyZonePricing?> Read(int entityId) => Task.FromResult(data.BodyZonePricings.FirstOrDefault(s => s.Id == entityId));

    /// <inheritdoc/> 
    public Task<IList<BodyZonePricing>> ReadAll() => Task.FromResult<IList<BodyZonePricing>>([.. data.BodyZonePricings]);

    /// <inheritdoc/> 
    public Task<BodyZonePricing> Update(BodyZonePricing entity)
    {
        Delete(entity.Id);
        Create(entity);
        return Task.FromResult(entity);
    }
}
