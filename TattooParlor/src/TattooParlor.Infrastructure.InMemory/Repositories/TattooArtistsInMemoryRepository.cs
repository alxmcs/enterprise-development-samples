using TattooParlor.Domain.Data;
using TattooParlor.Domain.Model.TattooArtists;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Infrastructure.InMemory.Repositories;

/// <summary>
/// Ин-мемори репозиторий для тату-мастеров
/// </summary>
public class TattooArtistsInMemoryRepository(DataSeeder data) : ITattooArtistManager
{
    /// <inheritdoc/> 
    public TattooArtist Create(TattooArtist entity)
    {
        entity.Id = data.TattooArtists.MaxBy(a => a.Id)!.Id + 1;
        data.TattooArtists.Add(entity);
        return entity;
    }

    /// <inheritdoc/> 
    public bool Delete(int entityId)
    {
        var res = data.TattooArtists.RemoveAll(s => s.Id == entityId);
        return res > 0;
    }

    /// <inheritdoc/> 
    public TattooArtist? Read(int entityId) => data.TattooArtists.FirstOrDefault(s => s.Id == entityId);

    /// <inheritdoc/> 
    public IList<TattooArtist> ReadAll() => [.. data.TattooArtists];

    /// <inheritdoc/> 
    public IList<TattooSession> Sessions(TattooArtist entity)
        => [.. data.TattooSessions.Where(s => s.TattooArtist.Id == entity.Id)];

    /// <inheritdoc/> 
    public IList<TattooArtist> ShowArtistsByFavoriteStyle(TattooStyle style) =>
        [.. data.TattooArtists.Where(a => a.FavoriteStyle == style)];

    /// <inheritdoc/> 
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

    /// <inheritdoc/> 
    public TattooArtist Update(TattooArtist entity)
    {
        Delete(entity.Id);
        Create(entity);
        return entity;
    }
}
