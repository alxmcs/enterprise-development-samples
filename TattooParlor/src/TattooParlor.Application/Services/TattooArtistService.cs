using MapsterMapper;
using TattooParlor.Application.Contracts.TattooArtists;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Domain.Model.TattooArtists;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Services;

/// <summary>
/// Аппликейшен служба для тату-мастеров
/// </summary>
/// <param name="manager">Доменная служба для тату-мастеров</param>
/// <param name="mapper">Маппер</param>
public class TattooArtistService(ITattooArtistManager manager, IMapper mapper) : ITattooArtistService
{
    /// <inheritdoc/>
    public async Task<TattooArtistDto> Create(TattooArtistCreateUpdateDto dto)
    {
        var result = await manager.Create(mapper.Map<TattooArtist>(dto));
        return mapper.Map<TattooArtistDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int dtoId) => await manager.Delete(dtoId);

    public async Task<TattooArtistDto?> Get(int dtoId)
    {
        var result = await manager.Read(dtoId);
        return result is null
            ? throw new KeyNotFoundException($"TattooArtist with Id {dtoId} not found")
            : mapper.Map<TattooArtistDto>(result);
    }

    /// <inheritdoc/>
    public async Task<IList<TattooArtistDto>> GetAll() => mapper.Map<IList<TattooArtistDto>>(await manager.ReadAll());

    /// <inheritdoc/>
    public async Task<IList<TattooArtistDto>> GetArtistsByFavoriteStyle(TattooStyle style)
        => mapper.Map<IList<TattooArtistDto>>(await manager.ShowArtistsByFavoriteStyle(style));

    /// <inheritdoc/>
    public async Task<IList<TattooSessionDto>> GetSessions(TattooArtistDto tattooArtist)
        => mapper.Map<IList<TattooSessionDto>>(await manager.Sessions(mapper.Map<TattooArtist>(tattooArtist)));

    /// <inheritdoc/>
    public async Task<IList<TattooArtistRevenueDto>> GetTop5ArtistsByRevenue(DateOnly startDate, DateOnly endDate)
        => mapper.Map<IList<TattooArtistRevenueDto>>(await manager.ShowTop5ArtistsByRevenue(startDate, endDate));

    /// <inheritdoc/>
    public async Task<TattooArtistDto> Update(TattooArtistCreateUpdateDto dto, int dtoId)
    {
        var entity = mapper.Map<TattooArtist>(dto);
        entity.Id = dtoId;
        var result =  await manager.Update(entity);
        return mapper.Map<TattooArtistDto>(result);
    }
}
