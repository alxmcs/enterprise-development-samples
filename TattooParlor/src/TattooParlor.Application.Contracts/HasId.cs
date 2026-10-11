namespace TattooParlor.Application.Contracts;

/// <summary>
/// Интерфейс для маркирования контрактов с идентификаторами
/// </summary>
/// <typeparam name="TKey">Тип идентификатора</typeparam>
public interface IHasId<TKey> where TKey : struct
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    TKey Id { get; }
}
