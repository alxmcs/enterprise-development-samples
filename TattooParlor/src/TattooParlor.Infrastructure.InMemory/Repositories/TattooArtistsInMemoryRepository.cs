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
    public Task<TattooArtist> Create(TattooArtist entity)
    {
        entity.Id = data.TattooArtists.Count == 0 ? 1 : data.TattooArtists.Max(a => a.Id) + 1;
        data.TattooArtists.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc/>
    public Task<bool> Delete(int entityId)
    {
        var res = data.TattooArtists.RemoveAll(s => s.Id == entityId);
        return Task.FromResult(res > 0);
    }

    /// <inheritdoc/>
    public Task<TattooArtist?> Read(int entityId) => Task.FromResult(data.TattooArtists.FirstOrDefault(s => s.Id == entityId));

    /// <inheritdoc/>
    public Task<IList<TattooArtist>> ReadAll() => Task.FromResult<IList<TattooArtist>>([.. data.TattooArtists]);

    /// <inheritdoc/>
    public Task<IList<TattooSession>> Sessions(TattooArtist entity)
        => Task.FromResult<IList<TattooSession>>([.. data.TattooSessions.Where(s => s.TattooArtist.Id == entity.Id)]);

    /// <inheritdoc/>
    public Task<IList<TattooArtist>> ShowArtistsByFavoriteStyle(TattooStyle style) =>
        Task.FromResult<IList<TattooArtist>>([.. data.TattooArtists.Where(a => a.FavoriteStyle == style)]);

    /// <inheritdoc/>
    public Task<IList<(TattooArtist Artist, decimal Money)>> ShowTop5ArtistsByRevenue(DateOnly startDate, DateOnly endDate)
    {
        var start = startDate.ToDateTime(TimeOnly.MinValue);
        var end = endDate.ToDateTime(TimeOnly.MaxValue);

        if (start > end)
            throw new ArgumentException("Start date must be less than or equal to end date");

        return Task.FromResult<IList<(TattooArtist Artist, decimal Money)>>([.. data.TattooSessions
            .Where(s => s.SessionDate >= start && s.SessionDate <= end && s.CancelDate == null)
            .GroupBy(s => s.TattooArtist.Id)
            .Select(g => (Artist: g.First().TattooArtist, Money: g.Sum(s => s.Price)))
            .OrderByDescending(x => x.Money).Take(5)]);
    }

    /// <inheritdoc/>
    public Task<TattooArtist> Update(TattooArtist entity)
    {
        Delete(entity.Id);
        Create(entity);
        return Task.FromResult(entity);
    }
}
