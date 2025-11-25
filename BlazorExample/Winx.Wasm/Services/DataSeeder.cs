using Winx.Wasm.Domain;

namespace Winx.Wasm.Services;

/// <summary>
/// Датасидер фей винкс
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Метод первоначального анполнения коллекции фей винкс
    /// </summary>
    /// <returns>Коллекция фей винкс</returns>
    public static List<Fairy> Seed() =>
        [
            new Fairy
            {
                Id = 1,
                Name = "Муза",
                PhotoUrl = "images/musa.jpg"
            },
            new Fairy
            {
                Id = 2,
                Name = "Стелла",
                PhotoUrl = "images/stella.jpg"
            },
            new Fairy
            {
                Id = 3,
                Name = "Блум",
                PhotoUrl = "images/bloom.jpg"
            },
            new Fairy
            {
                Id = 4,
                Name = "Флора",
                PhotoUrl = "images/flora.jpg"
            },
            new Fairy
            {
                Id = 5,
                Name = "Техна",
                PhotoUrl = "images/techna.jpg"
            },
            new Fairy
            {
                Id = 6,
                Name = "Лейла",
                PhotoUrl = "images/layla.jpg"
            }
        ];
}
