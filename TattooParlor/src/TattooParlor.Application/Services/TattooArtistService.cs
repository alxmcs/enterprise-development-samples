using MapsterMapper;
using TattooParlor.Application.Contracts.TattooArtists;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Domain.Model.TattooArtists;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Services;

public class TattooArtistService(ITattooArtistManager manager, IMapper mapper) : ITattooArtistService
{
    /// <inheritdoc/>
    public TattooArtistDto Create(TattooArtistCreateUpdateDto dto)
    {
        var result = manager.Create(mapper.Map<TattooArtist>(dto));
        return mapper.Map<TattooArtistDto>(result);
    }

    /// <inheritdoc/>
    public bool Delete(int dtoId) => manager.Delete(dtoId);

    public TattooArtistDto? Get(int dtoId)
    {
        var result = manager.Read(dtoId);
        return result is null
            ? throw new KeyNotFoundException($"TattooArtist with Id {dtoId} not found.")
            : mapper.Map<TattooArtistDto>(result);
    }

    /// <inheritdoc/>
    public IList<TattooArtistDto> GetAll() => mapper.Map<IList<TattooArtistDto>>(manager.ReadAll());

    /// <inheritdoc/>
    public IList<TattooArtistDto> GetArtistsByFavoriteStyle(TattooStyle style)
        => mapper.Map<IList<TattooArtistDto>>(manager.ShowArtistsByFavoriteStyle(style));

    /// <inheritdoc/>
    public IList<TattooSessionDto> GetSessions(TattooArtistDto tattooArtist)
        => mapper.Map<IList<TattooSessionDto>>(manager.Sessions(mapper.Map<TattooArtist>(tattooArtist)));

    /// <inheritdoc/>
    public IList<TattooArtistRevenueDto> GetTop5ArtistsByRevenue(DateOnly startDate, DateOnly endDate)
        => mapper.Map<IList<TattooArtistRevenueDto>>(manager.ShowTop5ArtistsByRevenue(startDate, endDate));

    /// <inheritdoc/>
    public TattooArtistDto Update(TattooArtistCreateUpdateDto dto, int dtoId)
    {
        var entity = mapper.Map<TattooArtist>(dto);
        entity.Id = dtoId;
        var result = manager.Update(entity);
        return mapper.Map<TattooArtistDto>(result);
    }
}
