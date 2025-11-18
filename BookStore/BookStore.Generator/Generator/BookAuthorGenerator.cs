using Bogus;
using BookStore.Application.Contracts.BookAuthors;


namespace BookStore.Generator.Generator;

/// <summary>
/// Статический класс для генерации контрактов
/// </summary>
public static class BookAuthorGenerator
{
    /// <summary>
    /// Метод для генерации заданного числа контрактов согласно правилам
    /// </summary>
    /// <param name="count">Заданное число контрактов</param>
    /// <returns>Коллекция контрактов</returns>
    /// <remarks>
    /// Генерация контрактов для взятой в этом примере доменной области крайне неудачная, но деваться уже некуда
    /// В вариантах лабораторной контракты выглядят гораздо более вменяемо, чем тут
    /// </remarks>
    public static List<BookAuthorCreateUpdateDto> GenerateLinks(int count) =>
        new Faker<BookAuthorCreateUpdateDto>()
            .WithRecord()
            .RuleFor(ba => ba.AuthorId, f => f.Random.Int(1, 4))
            .RuleFor(ba => ba.BookId, f => f.Random.Int(1, 4))
            .Generate(count);
}
