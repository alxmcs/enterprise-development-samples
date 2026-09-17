using TattooParlor.Domain.Data;
using TattooParlor.Domain.Shared.Enums;
using TattooParlor.Infrastructure.InMemory.Repositories;

namespace TattooParlor.Domain.Tests;

/// <summary>
/// Тесты доменной службы мастеров
/// </summary>
/// <param name="data">Датасид</param>
public class TattooArtistsManagerTests(DataSeeder data) : IClassFixture<DataSeeder>
{
    /// <summary>
    /// Проверка, что метод возвращает мастеров с заданным стилем
    /// </summary>
    /// <param name="style">Стиль</param>
    /// <param name="expectedCount">Ожидаемое количество</param>
    [Theory]
    [InlineData(TattooStyle.Realistic, 1)]
    [InlineData(TattooStyle.Watercolor, 1)]
    [InlineData(TattooStyle.Blackwork, 1)]
    public void ShowArtistsByFavoriteStyle_StyleExistsInSessions_ReturnsArtists(TattooStyle style, int expectedCount)
    {
        var repo = new TattooArtistsInMemoryRepository(data);

        var result = repo.ShowArtistsByFavoriteStyle(style);

        Assert.Equal(expectedCount, result.Count);
    }

    /// <summary>
    /// Проверка, что метод возвращает пустую коллекцию, если стиль не встречается в сессиях
    /// </summary>
    [Fact]
    public void ShowArtistsByFavoriteStyle_StyleDoesNotExistInSessions_ReturnsEmptyCollection()
    {
        var repo = new TattooArtistsInMemoryRepository(data);

        var result = repo.ShowArtistsByFavoriteStyle(TattooStyle.Minimalism);

        Assert.Empty(result);
    }

    /// <summary>
    /// Проверка, что метод возвращает топ-5 мастеров по выручке за указанный период
    /// </summary>
    /// <param name="startDate">Дата начала периода</param>
    /// <param name="endDate">Дата окончания периода</param>
    /// <param name="expectedCount">Ожидаемое количество</param>
    /// <param name="expectedRevenue">Ожидаемая выручка</param>
    [Theory]
    [InlineData("2026-01-01", "2026-12-31", 5, 14212.5)]
    [InlineData("2026-01-01", "2026-02-10", 2, 4914.00)]
    [InlineData("2026-03-01", "2026-04-20", 2, 1829.52)]
    public void ShowTop5ArtistsByRevenue_CorrectDates_ReturnsArtists(string startDate, string endDate, int expectedCount, decimal expectedRevenue)
    {
        var repo = new TattooArtistsInMemoryRepository(data);
        var start = DateOnly.Parse(startDate);
        var end = DateOnly.Parse(endDate);

        var result = repo.ShowTop5ArtistsByRevenue(start, end);

        Assert.Equal(expectedCount, result.Count);
        Assert.Equal(expectedRevenue, result[0].Money);
    }

    /// <summary>
    /// Проверка, что метод выбрасывает исключение, если дата начала позже даты окончания
    /// </summary>
    [Fact]
    public void ShowTop5ArtistsByRevenue_StartDateAfterEndDate_ThrowsArgumentException()
    {
        var repo = new TattooArtistsInMemoryRepository(data);
        var start = DateOnly.Parse("2026-12-31");
        var end = DateOnly.Parse("2026-01-01");

        Assert.Throws<ArgumentException>(() => repo.ShowTop5ArtistsByRevenue(start, end));
    }
}
