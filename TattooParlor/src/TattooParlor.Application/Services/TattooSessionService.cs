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
    public async Task<TattooSessionDto> Create(TattooSessionCreateUpdateDto dto)
    {
        var result = mapper.Map<TattooSession>(dto);
        result.Customer = await customerManager.Read(dto.CustomerId) ?? throw new KeyNotFoundException($"Cutomer with Id {dto.CustomerId} not found");
        result.TattooArtist = await artistManager.Read(dto.TattooArtistId) ?? throw new KeyNotFoundException($"Tattoo artist with Id {dto.TattooArtistId} not found");
        result.Style = (await styleRepo.ReadAll()).FirstOrDefault(s => s.Style == dto.Style) ?? throw new KeyNotFoundException($"Tattoo style {dto.Style} not found");
        result.BodyZone = (await zoneRepo.ReadAll()).FirstOrDefault(s => s.BodyZone == dto.BodyZone) ?? throw new KeyNotFoundException($"Body zone {dto.BodyZone} not found");

        result.StyleId = result.Style.Id;
        result.BodyZoneId = result.BodyZone.Id;

        await sessionManager.Create(result);
        return mapper.Map<TattooSessionDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(Guid dtoId) => await sessionManager.Delete(dtoId);

    /// <inheritdoc/>
    public async Task<TattooSessionDto?> Get(Guid dtoId)
    {
        var result = await sessionManager.Read(dtoId);
        return result is null
            ? throw new KeyNotFoundException($"TattooSession with Id {dtoId} not found")
            : mapper.Map<TattooSessionDto>(result);
    }

    /// <inheritdoc/>
    public async Task<IList<TattooSessionDto>> GetAll() => mapper.Map<IList<TattooSessionDto>>(await sessionManager.ReadAll());

    /// <inheritdoc/>
    public async Task<IList<TattooSessionDto>> GetAllNonRefundable()
        => mapper.Map<IList<TattooSessionDto>>(await sessionManager.ShowAllNonRefundable());

    /// <inheritdoc/>
    public async Task<TattooSessionRange> GetCostRange(TattooStyle style)
        => mapper.Map<TattooSessionRange>(await sessionManager.ShowCostRange(style));

    /// <inheritdoc/>
    public async Task<TattooSessionDto> Update(TattooSessionCreateUpdateDto dto, Guid dtoId)
    {
        var entity = mapper.Map<TattooSession>(dto);
        entity.Id = dtoId;
        var result = await sessionManager.Update(entity);
        return mapper.Map<TattooSessionDto>(result);
    }
}
