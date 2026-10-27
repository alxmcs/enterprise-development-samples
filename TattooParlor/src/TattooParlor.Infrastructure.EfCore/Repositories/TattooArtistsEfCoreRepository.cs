using Microsoft.EntityFrameworkCore;
using TattooParlor.Domain.Model.TattooArtists;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core репозиторий для тату-мастеров
/// </summary>
public class TattooArtistsEfCoreRepository(TattooParlorDbContext context) : ITattooArtistManager
{
    /// <inheritdoc/>
    public async Task<TattooArtist> Create(TattooArtist entity)
    {
        context.TattooArtists.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int entityId)
        => await context.TattooArtists.Where(a => a.Id == entityId).ExecuteDeleteAsync() > 0;

    /// <inheritdoc/>
    public async Task<TattooArtist?> Read(int entityId)
        => await context.TattooArtists.FirstOrDefaultAsync(a => a.Id == entityId);

    /// <inheritdoc/>
    public async Task<IList<TattooArtist>> ReadAll()
        => await context.TattooArtists.ToListAsync();

    /// <inheritdoc/>
    public async Task<IList<TattooSession>> Sessions(TattooArtist entity)
        => await context.TattooSessions
            .Where(s => s.TattooArtistId == entity.Id)
            .ToListAsync();

    /// <inheritdoc/>
    public async Task<IList<TattooArtist>> ShowArtistsByFavoriteStyle(TattooStyle style)
        => await context.TattooArtists.Where(a => a.FavoriteStyle == style).ToListAsync();

    /// <inheritdoc/>
    public async Task<IList<(TattooArtist Artist, decimal Money)>> ShowTop5ArtistsByRevenue(DateOnly startDate, DateOnly endDate)
    {
        var start = startDate.ToDateTime(TimeOnly.MinValue);
        var end = endDate.ToDateTime(TimeOnly.MaxValue);

        if (start > end)
            throw new ArgumentException("Дата начала не может быть позже даты окончания");

        var sessions = await context.TattooSessions
            .Where(s => s.SessionDate >= start && s.SessionDate <= end && s.CancelDate == null)
            .ToListAsync();

        return [.. sessions
            .GroupBy(s => s.TattooArtistId)
            .Select(g => (Artist: g.First().TattooArtist, Money: g.Sum(s => s.Price)))
            .OrderByDescending(x => x.Money)
            .Take(5)];
    }

    /// <inheritdoc/>
    public async Task<TattooArtist> Update(TattooArtist entity)
    {
        var existing = await context.TattooArtists.FirstOrDefaultAsync(a => a.Id == entity.Id)
            ?? throw new KeyNotFoundException($"Тату-мастер с Id {entity.Id} не найден");
        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existing;
    }
}
