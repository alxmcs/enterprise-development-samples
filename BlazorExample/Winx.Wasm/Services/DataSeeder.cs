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
                PhotoUrl = new Uri("images/musa.jpg")
            },
            new Fairy
            {
                Id = 2,
                Name = "Стелла",
                PhotoUrl = new Uri("images/stella.jpg")
            },
            new Fairy
            {
                Id = 3,
                Name = "Блум",
                PhotoUrl = new Uri("images/bloom.jpg")
            },
            new Fairy
            {
                Id = 4,
                Name = "Флора",
                PhotoUrl = new Uri("images/flora.jpg")
            },
            new Fairy
            {
                Id = 5,
                Name = "Техна",
                PhotoUrl = new Uri("images/techna.jpg")
            },
            new Fairy
            {
                Id = 6,
                Name = "Лейла",
                PhotoUrl = new Uri("images/layla.jpg")
            }
        ];
}
