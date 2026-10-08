using MapsterMapper;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Services;

/// <summary>
/// Аппликейшен служба для сессий
/// </summary>
/// <param name="manager">Доменная служба для сессий</param>
/// <param name="mapper">Маппер</param>
public class TattooSessionService(ITattooSessionManager manager, IMapper mapper) : ITattooSessionService
{
    /// <inheritdoc/>
    public TattooSessionDto Create(TattooSessionCreateUpdateDto dto)
    {
        var result = manager.Create(mapper.Map<TattooSession>(dto));
        return mapper.Map<TattooSessionDto>(result);
    }

    /// <inheritdoc/>
    public bool Delete(Guid dtoId) => manager.Delete(dtoId);

    /// <inheritdoc/>
    public TattooSessionDto? Get(Guid dtoId)
    {
        var result = manager.Read(dtoId);
        return result is null
            ? throw new KeyNotFoundException($"TattooSession with Id {dtoId} not found.")
            : mapper.Map<TattooSessionDto>(result);
    }

    /// <inheritdoc/>
    public IList<TattooSessionDto> GetAll() => mapper.Map<IList<TattooSessionDto>>(manager.ReadAll());

    /// <inheritdoc/>
    public IList<TattooSessionDto> GetAllNonRefundable()
        => mapper.Map<IList<TattooSessionDto>>(manager.ShowAllNonRefundable());

    /// <inheritdoc/>
    public TattooSessionRange GetCostRange(TattooStyle style)
        => mapper.Map<TattooSessionRange>(manager.ShowCostRange(style));

    /// <inheritdoc/>
    public TattooSessionDto Update(TattooSessionCreateUpdateDto dto, Guid dtoId)
    {
        var entity = mapper.Map<TattooSession>(dto);
        entity.Id = dtoId;
        var result = manager.Update(entity);
        return mapper.Map<TattooSessionDto>(result);
    }
}
