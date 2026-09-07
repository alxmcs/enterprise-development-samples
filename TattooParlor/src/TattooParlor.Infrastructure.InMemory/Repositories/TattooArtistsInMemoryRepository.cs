using TattooParlor.Domain.Data;
using TattooParlor.Domain.Model.TattooArtists;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Infrastructure.InMemory.Repositories;

public class TattooArtistsInMemoryRepository(DataSeeder data) : ITattooArtistManager
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

    public IList<TattooArtist> ShowArtistsByFavoriteStyle(TattooStyle style) =>
        [.. data.TattooArtists.Where(a => a.FavoriteStyle == style)];

    public IList<(TattooArtist Artist, decimal Money)> ShowTop5ArtistsByRevenue(DateOnly startDate, DateOnly endDate)
    {
        var start = startDate.ToDateTime(TimeOnly.MinValue);
        var end = endDate.ToDateTime(TimeOnly.MaxValue);

        if (start > end)
            throw new ArgumentException("Start date must be less than or equal to end date");

        return [.. data.TattooSessions
            .Where(s => s.SessionDate >= start && s.SessionDate <= end && s.CancelDate == null)
            .GroupBy(s => s.TattooArtist.Id)
            .Select(g => (Artist: g.First().TattooArtist, Money: g.Sum(s => s.Price)))
            .OrderByDescending(x => x.Money).Take(5)];
    }
    public TattooArtist Update(TattooArtist entity)
    {
        throw new NotImplementedException();
    }
}
