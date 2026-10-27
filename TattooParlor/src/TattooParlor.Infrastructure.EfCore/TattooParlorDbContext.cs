using Microsoft.EntityFrameworkCore;
using TattooParlor.Domain.Data;
using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Model.References;
using TattooParlor.Domain.Model.TattooArtists;
using TattooParlor.Domain.Model.TattooSessions;

namespace TattooParlor.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных тату-салона
/// </summary>
/// <param name="options">Параметры контекста</param>
public class TattooParlorDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    /// Клиенты
    /// </summary>
    public DbSet<Customer> Customers => Set<Customer>();

    /// <summary>
    /// Тату-мастера
    /// </summary>
    public DbSet<TattooArtist> TattooArtists => Set<TattooArtist>();

    /// <summary>
    /// Тату-сессии
    /// </summary>
    public DbSet<TattooSession> TattooSessions => Set<TattooSession>();

    /// <summary>
    /// Справочник стоимости по стилям
    /// </summary>
    public DbSet<StylePricing> StylePricings => Set<StylePricing>();

    /// <summary>
    /// Справочник стоимости по участкам тела
    /// </summary>
    public DbSet<BodyZonePricing> BodyZonePricings => Set<BodyZonePricing>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var seed = new DataSeeder();

        modelBuilder.Entity<StylePricing>(entity =>
        {
            entity.HasData(seed.StylePricings);
        });

        modelBuilder.Entity<BodyZonePricing>(entity =>
        {
            entity.HasData(seed.BodyZonePricings);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasData(seed.Customers);
        });

        modelBuilder.Entity<TattooArtist>(entity =>
        {
            entity.HasData(seed.TattooArtists);
        });

        modelBuilder.Entity<TattooSession>(entity =>
        {
            entity.HasData(seed.TattooSessions.Select(e => new
            {
                e.Id,
                e.CustomerId,
                e.TattooArtistId,
                e.BodyZoneId,
                e.StyleId,
                e.SessionDate,
                e.HeightInCm,
                e.WidthInCm,
                e.IsMulticolor,
                e.CancelDate
            }));
        });
    }
}
