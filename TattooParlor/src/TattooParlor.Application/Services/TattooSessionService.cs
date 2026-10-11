using MapsterMapper;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Model.References;
using TattooParlor.Domain.Model.TattooArtists;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Services;

/// <summary>
/// Аппликейшен служба для сессий
/// </summary>
/// <param name="sessionManager">Доменная служба для сессий</param>
/// <param name="customerManager">Доменная служба для клиентов</param>
/// <param name="artistManager">Доменная служба для тату-художников</param>
/// <param name="styleRepo">Репозиторий для стилей татуировки</param>
/// <param name="zoneRepo">Репозиторий для частей тела</param>
/// <param name="mapper">Маппер</param>
public class TattooSessionService(
    ITattooSessionManager sessionManager,
    ICustomerManager customerManager,
    ITattooArtistManager artistManager,
    ICrudRepository<StylePricing, int> styleRepo,
    ICrudRepository<BodyZonePricing, int> zoneRepo,
    IMapper mapper) : ITattooSessionService
{
    /// <inheritdoc/>
    public TattooSessionDto Create(TattooSessionCreateUpdateDto dto)
    {
        var result = mapper.Map<TattooSession>(dto);
        result.Customer = customerManager.Read(dto.CustomerId) ?? throw new KeyNotFoundException($"Cutomer with Id {dto.CustomerId} not found");
        result.TattooArtist = artistManager.Read(dto.TattooArtistId) ?? throw new KeyNotFoundException($"Tattoo artist with Id {dto.TattooArtistId} not found");
        result.Style = styleRepo.ReadAll().FirstOrDefault(s => s.Style == dto.Style) ?? throw new KeyNotFoundException($"Tattoo style {dto.Style} not found");
        result.BodyZone = zoneRepo.ReadAll().FirstOrDefault(s => s.BodyZone == dto.BodyZone) ?? throw new KeyNotFoundException($"Body zone {dto.BodyZone} not found");

        sessionManager.Create(result);
        return mapper.Map<TattooSessionDto>(result);
    }

    /// <inheritdoc/>
    public bool Delete(Guid dtoId) => sessionManager.Delete(dtoId);

    /// <inheritdoc/>
    public TattooSessionDto? Get(Guid dtoId)
    {
        var result = sessionManager.Read(dtoId);
        return result is null
            ? throw new KeyNotFoundException($"TattooSession with Id {dtoId} not found")
            : mapper.Map<TattooSessionDto>(result);
    }

    /// <inheritdoc/>
    public IList<TattooSessionDto> GetAll() => mapper.Map<IList<TattooSessionDto>>(sessionManager.ReadAll());

    /// <inheritdoc/>
    public IList<TattooSessionDto> GetAllNonRefundable()
        => mapper.Map<IList<TattooSessionDto>>(sessionManager.ShowAllNonRefundable());

    /// <inheritdoc/>
    public TattooSessionRange GetCostRange(TattooStyle style)
        => mapper.Map<TattooSessionRange>(sessionManager.ShowCostRange(style));

    /// <inheritdoc/>
    public TattooSessionDto Update(TattooSessionCreateUpdateDto dto, Guid dtoId)
    {
        var entity = mapper.Map<TattooSession>(dto);
        entity.Id = dtoId;
        var result = sessionManager.Update(entity);
        return mapper.Map<TattooSessionDto>(result);
    }
}
