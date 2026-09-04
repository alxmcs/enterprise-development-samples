using TattooParlor.Domain.Data;
using TattooParlor.Domain.Shared.Enums;
using TattooParlor.Infrastructure.InMemory.Repositories;

namespace TattooParlor.Domain.Tests;

public class CustomerManagerTests(DataSeeder data) : IClassFixture<DataSeeder>
{
    [Theory]
    [InlineData(TattooStyle.Blackwork, 1)]
    [InlineData(TattooStyle.Geometric, 2)]
    [InlineData(TattooStyle.Watercolor, 2)]
    public void ShowCustomersWithStyle_StyleExistsInSessions_ReturnsCustomers(TattooStyle style, int expectedCount)
    {
        var repo = new CustomerInMemoryRepository(data);

        var result = repo.ShowCustomersWithStyle(style);

        Assert.Equal(expectedCount, result.Count);
    }

    [Fact]
    public void ShowCustomersWithStyle_StyleDoesNotExistInSessions_ReturnsEmptyCollection()
    {
        var repo = new CustomerInMemoryRepository(data);

        var result = repo.ShowCustomersWithStyle(TattooStyle.Minimalism);

        Assert.Empty(result);
    }

    [Theory]
    [InlineData("2026-01-01", "2026-12-31", 5)]
    [InlineData("2026-01-01", "2026-02-10", 2)]
    [InlineData("2026-03-01", "2026-04-20", 2)]
    public void ShowTop5CustomersByVisits_CorrectDates_ReturnsCustomers(string startDate, string endDate, int expectedCount)
    {
        var repo = new CustomerInMemoryRepository(data);
        var start = DateOnly.Parse(startDate);
        var end = DateOnly.Parse(endDate);

        var result = repo.ShowTop5CustomersByVisits(start, end);

        Assert.Equal(expectedCount, result.Count);
    }

    [Fact]
    public void ShowTop5CustomersByVisits_StartDateAfterEndDate_ThrowsArgumentException()
    {
        var repo = new CustomerInMemoryRepository(data);
        var start = DateOnly.Parse("2026-12-31");
        var end = DateOnly.Parse("2026-01-01");

        Assert.Throws<ArgumentException>(() => repo.ShowTop5CustomersByVisits(start, end));
    }
}
