using TattooParlor.Domain.Data;
using TattooParlor.Domain.Shared.Enums;
using TattooParlor.Infrastructure.InMemory.Repositories;

namespace TattooParlor.Domain.Tests;

/// <summary>
/// Тесты доменной службы клиентов
/// </summary>
/// <param name="data">Датасид</param>
public class CustomerManagerTests(DataSeeder data) : IClassFixture<DataSeeder>
{
    /// <summary>
    /// Проверка, что метод возвращает клиентов с заданным стилем
    /// </summary>
    /// <param name="style">Стиль</param>
    /// <param name="expectedCount">Ожидаемое количество</param>
    [Theory]
    [InlineData(TattooStyle.Blackwork, 1)]
    [InlineData(TattooStyle.Geometric, 2)]
    [InlineData(TattooStyle.Watercolor, 2)]
    public async Task ShowCustomersWithStyle_StyleExistsInSessions_ReturnsCustomers(TattooStyle style, int expectedCount)
    {
        var repo = new CustomerInMemoryRepository(data);

        var result = await repo.ShowCustomersWithStyle(style);

        Assert.Equal(expectedCount, result.Count);
    }

    /// <summary>
    /// Проверка, что метод возвращает пустую коллекцию, если стиль не встречается в сессиях
    /// </summary>
    [Fact]
    public async Task ShowCustomersWithStyle_StyleDoesNotExistInSessions_ReturnsEmptyCollection()
    {
        var repo = new CustomerInMemoryRepository(data);

        var result = await repo.ShowCustomersWithStyle(TattooStyle.Minimalism);

        Assert.Empty(result);
    }

    /// <summary>
    /// Проверка, что метод возвращает топ-5 клиентов по количеству визитов за указанный период
    /// </summary>
    /// <param name="startDate">Дата начала периода</param>
    /// <param name="endDate">Дата окончания периода</param>
    /// <param name="expectedCount">Ожидаемое количество</param>
    [Theory]
    [InlineData("2026-01-01", "2026-12-31", 5)]
    [InlineData("2026-01-01", "2026-02-10", 2)]
    [InlineData("2026-03-01", "2026-04-20", 2)]
    public async Task ShowTop5CustomersByVisits_CorrectDates_ReturnsCustomers(string startDate, string endDate, int expectedCount)
    {
        var repo = new CustomerInMemoryRepository(data);
        var start = DateOnly.Parse(startDate);
        var end = DateOnly.Parse(endDate);

        var result = await repo.ShowTop5CustomersByVisits(start, end);

        Assert.Equal(expectedCount, result.Count);
    }

    /// <summary>
    /// Проверка, что метод выбрасывает исключение, если дата начала позже даты окончания
    /// </summary>
    [Fact]
    public async Task ShowTop5CustomersByVisits_StartDateAfterEndDate_ThrowsArgumentException()
    {
        var repo = new CustomerInMemoryRepository(data);
        var start = DateOnly.Parse("2026-12-31");
        var end = DateOnly.Parse("2026-01-01");

        await Assert.ThrowsAsync<ArgumentException>(() => repo.ShowTop5CustomersByVisits(start, end));
    }
}
