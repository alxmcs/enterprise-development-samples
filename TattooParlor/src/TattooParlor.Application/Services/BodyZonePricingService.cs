using MapsterMapper;
using TattooParlor.Application.Contracts;
using TattooParlor.Application.Contracts.BodyZonePricings;
using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Model.References;

namespace TattooParlor.Application.Services;

/// <summary>
/// Аппликейшен служба для справочника частей тела
/// </summary>
/// <param name="manager">Доменная служба для справочника частей тела</param>
/// <param name="mapper">Маппер</param>
public class BodyZonePricingService(ICrudRepository<BodyZonePricing, int> manager, IMapper mapper) : IApplicationService<BodyZonePricingDto, BodyZonePricingCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public async Task<BodyZonePricingDto> Create(BodyZonePricingCreateUpdateDto dto)
    {
        var result = await manager.Create(mapper.Map<BodyZonePricing>(dto));
        return mapper.Map<BodyZonePricingDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int dtoId) => await manager.Delete(dtoId);

    /// <inheritdoc/>
    public async Task<BodyZonePricingDto?> Get(int dtoId)
    {
        var result = await manager.Read(dtoId);
        return result is null
            ? throw new KeyNotFoundException($"BodyZonePricing with Id {dtoId} not found")
            : mapper.Map<BodyZonePricingDto>(result);
    }

    /// <inheritdoc/>
    public async Task<IList<BodyZonePricingDto>> GetAll() => mapper.Map<IList<BodyZonePricingDto>>(await manager.ReadAll());

    /// <inheritdoc/>
    public async Task<BodyZonePricingDto> Update(BodyZonePricingCreateUpdateDto dto, int dtoId)
    {
        var entity = mapper.Map<BodyZonePricing>(dto);
        entity.Id = dtoId;
        var result = await manager.Update(entity);
        return mapper.Map<BodyZonePricingDto>(result);
    }
}
