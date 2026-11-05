using Bogus;
using System.Runtime.CompilerServices;

namespace TattooParlor.Generator.Domain;

/// <summary>
/// Класс-расширение для генератора контрактов
/// </summary>
public static class SessionGeneratorExtensions
{
    /// <summary>
    /// Метод для облегчения генерации record-ов
    /// </summary>
    /// <typeparam name="T">Параметр типа генерируемых данных</typeparam>
    /// <param name="faker">Генератор данных</param>
    public static Faker<T> WithRecord<T>(this Faker<T> faker) where T : class =>
        faker.CustomInstantiator(_ => (RuntimeHelpers.GetUninitializedObject(typeof(T)) as T)!);
}