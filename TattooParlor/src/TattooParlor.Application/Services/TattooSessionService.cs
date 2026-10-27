using MapsterMapper;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Services;

/// <summary>
/// Аппликейшен служба для сессий
/// </summary>
/// <param name="sessionManager">Доменная служба для сессий</param>
/// <param name="mapper">Маппер</param>
public class TattooSessionService(ITattooSessionManager sessionManager, IMapper mapper) : ITattooSessionService
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
}
