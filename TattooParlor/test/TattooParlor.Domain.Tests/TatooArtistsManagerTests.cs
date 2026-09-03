using TattooParlor.Domain.Data;
using TattooParlor.Domain.Shared.Enums;
using TattooParlor.Infrastructure.InMemory.Repositories;

namespace TattooParlor.Domain.Tests;

public class TatooArtistsManagerTests(DataSeeder data) : IClassFixture<DataSeeder>
{
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

    [Fact]
    public void ShowArtistsByFavoriteStyle_StyleDoesNotExistInSessions_ReturnsEmptyCollection()
    {
        var repo = new TattooArtistsInMemoryRepository(data);

        var result = repo.ShowArtistsByFavoriteStyle(TattooStyle.Minimalism);

        Assert.Empty(result);
    }

    [Theory]
    [InlineData("2026-01-01", "2026-12-31", 5, 16344.72)]
    [InlineData("2026-01-01", "2026-02-10", 2, 4914.00)]
    [InlineData("2026-03-01", "2026-04-20", 4, 11430.72)]
    public void ShowTop5ArtistsByRevenue_CorrectDates_ReturnsArtists(string startDate, string endDate, int expectedCount, decimal expectedRevenue)
    {
        var repo = new TattooArtistsInMemoryRepository(data);
        var start = DateOnly.Parse(startDate);
        var end = DateOnly.Parse(endDate);

        var result = repo.ShowTop5ArtistsByRevenue(start, end);

        Assert.Equal(expectedCount, result.Count);
        Assert.Equal(expectedRevenue, result[0].Money);
    }

    [Fact]
    public void ShowTop5ArtistsByRevenue_StartDateAfterEndDate_ThrowsArgumentException()
    {
        var repo = new TattooArtistsInMemoryRepository(data);
        var start = DateOnly.Parse("2026-12-31");
        var end = DateOnly.Parse("2026-01-01");

        Assert.Throws<ArgumentException>(() => repo.ShowTop5ArtistsByRevenue(start, end));
    }
}
