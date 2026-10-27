using MapsterMapper;
using TattooParlor.Application.Contracts;
using TattooParlor.Application.Contracts.StylePricings;
using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Model.References;

namespace TattooParlor.Application.Services;

/// <summary>
/// Аппликейшен служба для справочника стилей
/// </summary>
/// <param name="manager">Доменная служба для справочника стилей</param>
/// <param name="mapper">Маппер</param>
public class StylePricingService(ICrudRepository<StylePricing, int> manager, IMapper mapper) : IApplicationService<StylePricingDto, StylePricingCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public async Task<StylePricingDto> Create(StylePricingCreateUpdateDto dto)
    {
        var result = await manager.Create(mapper.Map<StylePricing>(dto));
        return mapper.Map<StylePricingDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int dtoId) => await manager.Delete(dtoId);

    /// <inheritdoc/>
    public async Task<StylePricingDto?> Get(int dtoId)
    {
        var result = await manager.Read(dtoId);
        return result is null
            ? throw new KeyNotFoundException($"StylePricing with Id {dtoId} not found")
            : mapper.Map<StylePricingDto>(result);
    }

    /// <inheritdoc/>
    public async Task<IList<StylePricingDto>> GetAll() => mapper.Map<IList<StylePricingDto>>(await manager.ReadAll());

    /// <inheritdoc/>
    public async Task<StylePricingDto> Update(StylePricingCreateUpdateDto dto, int dtoId)
    {
        var entity = mapper.Map<StylePricing>(dto);
        entity.Id = dtoId;
        var result = await manager.Update(entity);
        return mapper.Map<StylePricingDto>(result);
    }
}
