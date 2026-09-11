using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Model.References;
using TattooParlor.Domain.Model.TattooArtists;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Data;

/// <summary>
/// Датасидер для первичного наполнения тествыми данными
/// </summary>
public class DataSeeder
{
    /// <summary>
    /// Список клиентов
    /// </summary>
    public List<Customer> Customers { get; set; }

    /// <summary>
    /// Список мастеров
    /// </summary>
    public List<TattooArtist> TattooArtists { get; set; }

    /// <summary>
    /// Список сеансов
    /// </summary>
    public List<TattooSession> TattooSessions { get; set; }

    /// <summary>
    /// Список доступных стилей
    /// </summary>
    public List<StylePricing> StylePricings { get; set; }

    /// <summary>
    /// Список доступных зон для татуировки
    /// </summary>
    public List<BodyZonePricing> BodyZonePricings { get; set; }

    public DataSeeder()
    {
        StylePricings =
        [
            new StylePricing { Id = 1, Style = TattooStyle.Realistic, PricePerSqCm = 25m, MulticolorMultiplier = 1.5m },
            new StylePricing { Id = 2, Style = TattooStyle.Watercolor, PricePerSqCm = 30m, MulticolorMultiplier = 1.8m },
            new StylePricing { Id = 3, Style = TattooStyle.Blackwork, PricePerSqCm = 20m },
            new StylePricing { Id = 4, Style = TattooStyle.Geometric, PricePerSqCm = 22m },
            new StylePricing { Id = 5, Style = TattooStyle.OldSchool, PricePerSqCm = 18m, MulticolorMultiplier = 1.3m },
            new StylePricing { Id = 6, Style = TattooStyle.Minimalism, PricePerSqCm = 15m },
            new StylePricing { Id = 7, Style = TattooStyle.Tribal, PricePerSqCm = 19m }
        ];

        BodyZonePricings =
        [
            new BodyZonePricing { Id = 1, BodyZone = BodyZone.Forearm, Multiplier = 1.0m },
            new BodyZonePricing { Id = 2, BodyZone = BodyZone.Shoulder, Multiplier = 1.1m },
            new BodyZonePricing { Id = 3, BodyZone = BodyZone.Arm, Multiplier = 1.0m },
            new BodyZonePricing { Id = 4, BodyZone = BodyZone.Chest, Multiplier = 1.3m },
            new BodyZonePricing { Id = 5, BodyZone = BodyZone.Belly, Multiplier = 1.2m },
            new BodyZonePricing { Id = 6, BodyZone = BodyZone.Hip, Multiplier = 1.2m },
            new BodyZonePricing { Id = 7, BodyZone = BodyZone.Thigh, Multiplier = 1.1m },
            new BodyZonePricing { Id = 8, BodyZone = BodyZone.Calf, Multiplier = 1.0m },
            new BodyZonePricing { Id = 9, BodyZone = BodyZone.Face, Multiplier = 2.0m },
            new BodyZonePricing { Id = 10, BodyZone = BodyZone.Back, Multiplier = 1.4m },
            new BodyZonePricing { Id = 11, BodyZone = BodyZone.Shin, Multiplier = 1.0m }
        ];

        TattooArtists =
        [
            new TattooArtist
            {
                Id = 1,
                FirstName = "Igor",
                LastName = "Volkov",
                DateOfBirth = new DateOnly(1988, 3, 14),
                PhoneNumber = "+79261234567",
                Email = "igor.volkov@tattooparlor.com",
                WorkStartDate = new DateOnly(2012, 6, 1),
                Rating = 4.8,
                FavoriteStyle = TattooStyle.Realistic
            },
            new TattooArtist
            {
                Id = 2,
                FirstName = "Marina",
                LastName = "Sokolova",
                DateOfBirth = new DateOnly(1992, 7, 22),
                PhoneNumber = "+79261234568",
                Email = "marina.sokolova@tattooparlor.com",
                WorkStartDate = new DateOnly(2016, 2, 15),
                Rating = 4.6,
                FavoriteStyle = TattooStyle.Watercolor
            },
            new TattooArtist
            {
                Id = 3,
                FirstName = "Denis",
                LastName = "Orlov",
                DateOfBirth = new DateOnly(1985, 11, 3),
                PhoneNumber = "+79261234569",
                Email = "denis.orlov@tattooparlor.com",
                WorkStartDate = new DateOnly(2009, 9, 10),
                Rating = 4.9,
                FavoriteStyle = TattooStyle.Blackwork
            },
            new TattooArtist
            {
                Id = 4,
                FirstName = "Anna",
                LastName = "Belova",
                DateOfBirth = new DateOnly(1995, 1, 30),
                PhoneNumber = "+79261234570",
                Email = "anna.belova@tattooparlor.com",
                WorkStartDate = new DateOnly(2020, 4, 5),
                Rating = 4.3,
                FavoriteStyle = null
            },
            new TattooArtist
            {
                Id = 5,
                FirstName = "Pavel",
                LastName = "Kuznetsov",
                DateOfBirth = new DateOnly(1990, 5, 18),
                PhoneNumber = "+79261234571",
                Email = "pavel.kuznetsov@tattooparlor.com",
                WorkStartDate = new DateOnly(2014, 8, 20),
                Rating = 4.7,
                FavoriteStyle = TattooStyle.Geometric
            }
        ];

        Customers =
        [
            new Customer
            {
                Id = 1,
                FirstName = "Alexey",
                LastName = "Petrov",
                DateOfBirth = new DateOnly(1990, 2, 10),
                PhoneNumber = "+79161112233",
                Email = "alexey.petrov@mail.com",
                FirstVisitDate = new DateOnly(2021, 5, 3),
                VisitCount = 4,
                HasAllergies = false
            },
            new Customer
            {
                Id = 2,
                FirstName = "Olga",
                LastName = "Ivanova",
                DateOfBirth = new DateOnly(1994, 8, 25),
                PhoneNumber = "+79161112234",
                Email = "olga.ivanova@mail.com",
                FirstVisitDate = new DateOnly(2022, 1, 17),
                VisitCount = 2,
                HasAllergies = true
            },
            new Customer
            {
                Id = 3,
                FirstName = "Sergey",
                LastName = "Smirnov",
                DateOfBirth = new DateOnly(1987, 12, 1),
                PhoneNumber = "+79161112235",
                Email = "sergey.smirnov@mail.com",
                FirstVisitDate = new DateOnly(2020, 9, 12),
                VisitCount = 7,
                HasAllergies = false
            },
            new Customer
            {
                Id = 4,
                FirstName = "Ekaterina",
                LastName = "Novikova",
                DateOfBirth = new DateOnly(1998, 4, 6),
                PhoneNumber = "+79161112236",
                Email = "ekaterina.novikova@mail.com",
                FirstVisitDate = new DateOnly(2023, 3, 21),
                VisitCount = 1,
                HasAllergies = false
            },
            new Customer
            {
                Id = 5,
                FirstName = "Dmitry",
                LastName = "Fedorov",
                DateOfBirth = new DateOnly(1983, 6, 19),
                PhoneNumber = "+79161112237",
                Email = "dmitry.fedorov@mail.com",
                FirstVisitDate = new DateOnly(2019, 11, 30),
                VisitCount = 10,
                HasAllergies = true
            },
            new Customer
            {
                Id = 6,
                FirstName = "Yulia",
                LastName = "Morozova",
                DateOfBirth = new DateOnly(2000, 9, 9),
                PhoneNumber = "+79161112238",
                Email = "yulia.morozova@mail.com",
                FirstVisitDate = new DateOnly(2023, 7, 8),
                VisitCount = 1
            }
        ];

        TattooSessions =
        [
            new TattooSession
            {
                Id = Guid.NewGuid(),
                Customer = Customers[0],
                TattooArtist = TattooArtists[0],
                SessionDate = new DateTime(2026, 1, 10, 14, 0, 0),
                BodyZone = BodyZonePricings[0],
                Style = StylePricings[1],
                HeightInCm = 10,
                WidthInCm = 8,
            },
            new TattooSession
            {
                Id = Guid.NewGuid(),
                Customer = Customers[1],
                TattooArtist = TattooArtists[1],
                SessionDate = new DateTime(2026, 2, 5, 11, 30, 0),
                BodyZone = BodyZonePricings[3],
                Style = StylePricings[1],
                HeightInCm = 15,
                WidthInCm = 12,
            },
            new TattooSession
            {
                Id = Guid.NewGuid(),
                Customer = Customers[2],
                TattooArtist = TattooArtists[2],
                SessionDate = new DateTime(2026, 2, 20, 16, 0, 0),
                BodyZone = BodyZonePricings[9],
                Style = StylePricings[2],
                HeightInCm = 20,
                WidthInCm = 20
            },
            new TattooSession
            {
                Id = Guid.NewGuid(),
                Customer = Customers[3],
                TattooArtist = TattooArtists[3],
                SessionDate = new DateTime(2026, 3, 1, 13, 0, 0),
                BodyZone = BodyZonePricings[6],
                Style = StylePricings[3],
                HeightInCm = 8,
                WidthInCm = 6,
            },
            new TattooSession
            {
                Id = Guid.NewGuid(),
                Customer = Customers[4],
                TattooArtist = TattooArtists[4],
                SessionDate = new DateTime(2026, 3, 15, 10, 0, 0),
                BodyZone = BodyZonePricings[1],
                Style = StylePricings[3],
                HeightInCm = 12,
                WidthInCm = 9,
            },
            new TattooSession
            {
                Id = Guid.NewGuid(),
                Customer = Customers[0],
                TattooArtist = TattooArtists[1],
                SessionDate = new DateTime(2026, 4, 2, 15, 0, 0),
                BodyZone = BodyZonePricings[4],
                Style = StylePricings[1],
                HeightInCm = 18,
                WidthInCm = 14,
                IsMulticolor = true,
                CancelDate = new DateTime(2026, 3, 25, 9, 0, 0)
            },
            new TattooSession
            {
                Id = Guid.NewGuid(),
                Customer = Customers[5],
                TattooArtist = TattooArtists[2],
                SessionDate = new DateTime(2026, 4, 20, 12, 0, 0),
                BodyZone = BodyZonePricings[7],
                Style = StylePricings[6],
                HeightInCm = 6,
                WidthInCm = 5,
                CancelDate = new DateTime(2026, 4, 19, 18, 0, 0)
            },
            new TattooSession
            {
                Id = Guid.NewGuid(),
                Customer = Customers[2],
                TattooArtist = TattooArtists[0],
                SessionDate = new DateTime(2026, 5, 5, 17, 0, 0),
                BodyZone = BodyZonePricings[0],
                Style = StylePricings[0],
                HeightInCm = 25,
                WidthInCm = 18,
                IsMulticolor = true
            }
        ];
    }
}
