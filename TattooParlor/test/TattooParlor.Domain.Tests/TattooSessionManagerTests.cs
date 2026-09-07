using TattooParlor.Domain.Data;
using TattooParlor.Domain.Shared.Enums;
using TattooParlor.Infrastructure.InMemory.Repositories;

namespace TattooParlor.Domain.Tests;

public class TattooSessionManagerTests(DataSeeder data) : IClassFixture<DataSeeder>
{

    [Fact]
    public void ShowAllNonRefundable_Sessions_ReturnsNonRefundableSessions()
    {
        var repo = new TattooSessionsInMemoryRepository(data);

        Assert.NotEmpty(result);
        Assert.All(result, s => Assert.True(s.IsPrepaymentNonRefundable));

    [Theory]
    [InlineData(TattooStyle.Realistic, 11812.5, 11812.5, 11812.5)]
    [InlineData(TattooStyle.Watercolor, 2400, 11430.72, 6248.24)]
    [InlineData(TattooStyle.Blackwork, 7840, 7840, 7840)]
    [InlineData(TattooStyle.Minimalism, 0, 0, 0)]
    public void ShowCostRange_ReturnsValues(TattooStyle style, decimal expectedMin, decimal expectedMax, decimal expectedAverage)
    {
        var repo = new TattooSessionsInMemoryRepository(data);

        var (min, max, average) = repo.ShowCostRange(style);

        Assert.Equal(expectedMin, min);
        Assert.Equal(expectedMax, max);
        Assert.Equal(expectedAverage, average);
    }
}
