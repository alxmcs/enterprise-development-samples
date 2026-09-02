using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Model.TattooArtists;

public interface ITattooArtistManager : IСrudRepository<TattooArtist, int>, IHasSessions<TattooArtist>
{
    IList<(TattooArtist Artist, decimal Money)> ShowTop5ArtistsByRevenue(DateOnly startDate, DateOnly endDate);

    IList<TattooArtist> ShowArtistsByFavoriteStyle(TattooStyle style);
}

