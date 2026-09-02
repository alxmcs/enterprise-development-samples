using TattooParlor.Domain.Model.TattooArtists;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Infrastructure.InMemory.Repositories;

public class TattooArtistsRepository : ITattooArtistManager
{
    public TattooArtist Create(TattooArtist entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int entityId)
    {
        throw new NotImplementedException();
    }

    public TattooArtist? Read(int entityId)
    {
        throw new NotImplementedException();
    }

    public IList<TattooArtist> ReadAll()
    {
        throw new NotImplementedException();
    }

    public IList<TattooSession> Sessions(TattooArtist entity)
    {
        throw new NotImplementedException();
    }

    public IList<TattooArtist> ShowArtistsByFavoriteStyle(TattooStyle style)
    {
        throw new NotImplementedException();
    }

    public IList<(TattooArtist Artist, decimal Money)> ShowTop5ArtistsByRevenue(DateOnly startDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }

    public TattooArtist Update(TattooArtist entity)
    {
        throw new NotImplementedException();
    }
}
