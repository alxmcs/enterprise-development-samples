using MapsterMapper;
using Microsoft.Extensions.Logging;
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
/// <param name="tattooArtistManager">Доменная служба для тату-мастеров</param>
/// <param name="styleManager">Доменная служба для стилей</param>
/// <param name="zoneManager">Доменная служба для зон тела</param>
/// <param name="mapper">Маппер</param>
/// <param name="logger">Логгер</param>
public class TattooSessionService(ITattooSessionManager sessionManager,
    ICustomerManager customerManager,
    ITattooArtistManager tattooArtistManager,
    ICrudRepository<StylePricing, int> styleManager,
    ICrudRepository<BodyZonePricing, int> zoneManager,
    IMapper mapper,
    ILogger<TattooSessionService> logger) : ITattooSessionService
{
    /// <inheritdoc/>
    public async Task<TattooSessionDto> Create(TattooSessionCreateUpdateDto dto)
    {
        var result = await sessionManager.Create(mapper.Map<TattooSession>(dto));
        return mapper.Map<TattooSessionDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(Guid dtoId) => await sessionManager.Delete(dtoId);

    /// <inheritdoc/>
    public async Task<TattooSessionDto?> Get(Guid dtoId)
    {
        var result = await sessionManager.Read(dtoId);
        return result is null
            ? throw new KeyNotFoundException($"Тату-сессия с Id {dtoId} не найдена")
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

    /// <inheritdoc/>
    public async Task ReceiveSessions(IList<TattooSessionCreateUpdateDto> sessions, CancellationToken cancellationToken)
    {
        var customerIds = (await customerManager.ReadAll()).Select(c => c.Id).ToHashSet();
        var artistIds = (await tattooArtistManager.ReadAll()).Select(a => a.Id).ToHashSet();
        var addedStyles = (await styleManager.ReadAll()).Select(s => s.Style).ToHashSet();
        var addedBodyZones = (await zoneManager.ReadAll()).Select(z => z.BodyZone).ToHashSet();

        var received = 0;
        var skipped = 0;
        foreach (var dto in sessions)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!customerIds.Contains(dto.CustomerId))
            {
                logger.LogWarning("Ошибка валидации: Клиент с id {customerId} не существует {@contract}", dto.CustomerId, dto);
                skipped++;
                continue;
            }
            if (!artistIds.Contains(dto.TattooArtistId))
            {
                logger.LogWarning("Ошибка валидации: Тату-мастер с id {artistId} не существует {@contract}", dto.TattooArtistId, dto);
                skipped++;
                continue;
            }
            if (dto.SessionDate < DateTime.Today)
            {
                logger.LogWarning("Ошибка валидации: Дата сессии {sessionDate} находится в прошлом {@contract}", dto.SessionDate, dto);
                skipped++;
                continue;
            }
            if (!addedStyles.Contains(dto.Style))
            {
                logger.LogWarning("Ошибка валидации: Для стиля {style} не определен ценник {@contract}", dto.Style, dto);
                skipped++;
                continue;
            }
            if (!addedBodyZones.Contains(dto.BodyZone))
            {
                logger.LogWarning("Ошибка валидации: Для зоны {zone} не определен ценник {@contract}", dto.BodyZone, dto);
                skipped++;
                continue;
            }
            await sessionManager.Create(mapper.Map<TattooSession>(dto));
            received++;
        }

        logger.LogInformation("Приём сессий завершён: принято {received}, пропущено {skipped} из {total} контрактов", received, skipped, sessions.Count);
    }
}
