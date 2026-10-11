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
    public StylePricingDto Create(StylePricingCreateUpdateDto dto)
    {
        var result = manager.Create(mapper.Map<StylePricing>(dto));
        return mapper.Map<StylePricingDto>(result);
    }

    /// <inheritdoc/>
    public bool Delete(int dtoId) => manager.Delete(dtoId);

    /// <inheritdoc/>
    public StylePricingDto? Get(int dtoId)
    {
        var result = manager.Read(dtoId);
        return result is null
            ? throw new KeyNotFoundException($"StylePricing with Id {dtoId} not found")
            : mapper.Map<StylePricingDto>(result);
    }

    /// <inheritdoc/>
    public IList<StylePricingDto> GetAll() => mapper.Map<IList<StylePricingDto>>(manager.ReadAll());

    /// <inheritdoc/>
    public StylePricingDto Update(StylePricingCreateUpdateDto dto, int dtoId)
    {
        var entity = mapper.Map<StylePricing>(dto);
        entity.Id = dtoId;
        var result = manager.Update(entity);
        return mapper.Map<StylePricingDto>(result);
    }
}
