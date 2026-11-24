using Bogus;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Generator.Domain;

/// <summary>
/// Статический класс для генерации контрактов
/// </summary>
public static class SessionGenerator
{
    /// <summary>
    /// Метод для генерации заданного числа контрактов согласно правилам
    /// </summary>
    /// <param name="count">Заданное число контрактов</param>
    /// <returns>Коллекция контрактов</returns>
    public static List<TattooSessionCreateUpdateDto> GenerateLinks(int count) =>
        new Faker<TattooSessionCreateUpdateDto>()
            .WithRecord()
            .RuleFor(ts => ts.CustomerId, f => f.Random.Int(1, 15))
            .RuleFor(ts => ts.TattooArtistId, f => f.Random.Int(1, 15))
            .RuleFor(ts => ts.SessionDate, f => f.Date.Between(DateTime.Now.AddDays(-5), DateTime.Now.AddDays(30)))
            .RuleFor(ts => ts.BodyZone, f => f.PickRandom<BodyZone>())
            .RuleFor(ts => ts.Style, f => f.PickRandom<TattooStyle>())
            .RuleFor(ts => ts.HeightInCm, f => Math.Round(f.Random.Double(0.1, 500), 2))
            .RuleFor(ts => ts.WidthInCm, f => Math.Round(f.Random.Double(0.1, 500), 2))
            .RuleFor(ts => ts.IsMulticolor, f => f.Random.Bool())
            .RuleFor(ts => ts.CancelDate, (f, ts) => f.Random.Bool() ? f.Date.Between(DateTime.Now, ts.SessionDate) : null)
            .Generate(count);
}
