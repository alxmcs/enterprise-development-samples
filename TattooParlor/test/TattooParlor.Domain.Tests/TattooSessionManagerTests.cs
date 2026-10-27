using TattooParlor.Domain.Data;
using TattooParlor.Domain.Shared.Enums;
using TattooParlor.Infrastructure.InMemory.Repositories;

namespace TattooParlor.Domain.Tests;

/// <summary>
/// Тесты доменной службы сессий
/// </summary>
/// <param name="data">Датасид</param>
public class TattooSessionManagerTests(DataSeeder data) : IClassFixture<DataSeeder>
{
    /// <summary>
    /// Проверка, что метод возвращает все сессии, которые не подлежат возврату
    /// </summary>
    [Fact]
    public async Task ShowAllNonRefundable_Sessions_ReturnsNonRefundableSessions()
    {
        var repo = new TattooSessionsInMemoryRepository(data);

        var result = await repo.ShowAllNonRefundable();

        Assert.NotEmpty(result);
        Assert.All(result, s => Assert.True(s.IsPrepaymentNonRefundable));
    }

    /// <summary>
    /// Проверка того, что метод возвращает диапазон стоимости татуировок по стилю
    /// </summary>
    /// <param name="style">Стиль</param>
    /// <param name="expectedMin">Ожидаемое минимальное значение</param>
    /// <param name="expectedMax">Ожидаемое максимальное значение</param>
    /// <param name="expectedAverage">Ожидаемое среднее значение</param>
    [Theory]
    [InlineData(TattooStyle.Realistic, 11812.5, 11812.5, 11812.5)]
    [InlineData(TattooStyle.Watercolor, 2400, 11430.72, 6248.24)]
    [InlineData(TattooStyle.Blackwork, 7840, 7840, 7840)]
    [InlineData(TattooStyle.Minimalism, 0, 0, 0)]
    public async Task ShowCostRange_ReturnsValues(TattooStyle style, decimal expectedMin, decimal expectedMax, decimal expectedAverage)
    {
        var repo = new TattooSessionsInMemoryRepository(data);

        var (min, max, average) = await repo.ShowCostRange(style);

        Assert.Equal(expectedMin, min);
        Assert.Equal(expectedMax, max);
        Assert.Equal(expectedAverage, average);
    }
}
