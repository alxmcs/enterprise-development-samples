using MapsterMapper;
using TattooParlor.Application.Contracts;
using TattooParlor.Application.Contracts.BodyZonePricings;
using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Model.References;

namespace TattooParlor.Application.Services;

public class BodyZonePricingService(ICrudRepository<BodyZonePricing, int> manager, IMapper mapper) : IApplicationService<BodyZonePricingDto, BodyZonePricingCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public BodyZonePricingDto Create(BodyZonePricingCreateUpdateDto dto)
    {
        var result = manager.Create(mapper.Map<BodyZonePricing>(dto));
        return mapper.Map<BodyZonePricingDto>(result);
    }

    /// <inheritdoc/>
    public bool Delete(int dtoId) => manager.Delete(dtoId);

    /// <inheritdoc/>
    public BodyZonePricingDto? Get(int dtoId)
    {
        var result = manager.Read(dtoId);
        return result is null
            ? throw new KeyNotFoundException($"BodyZonePricing with Id {dtoId} not found.")
            : mapper.Map<BodyZonePricingDto>(result);
    }

    /// <inheritdoc/>
    public IList<BodyZonePricingDto> GetAll() => mapper.Map<IList<BodyZonePricingDto>>(manager.ReadAll());
    
    /// <inheritdoc/>
    public BodyZonePricingDto Update(BodyZonePricingCreateUpdateDto dto, int dtoId)
    {
        var entity = mapper.Map<BodyZonePricing>(dto);
        entity.Id = dtoId;
        var result = manager.Update(entity);
        return mapper.Map<BodyZonePricingDto>(result);
    }
}
