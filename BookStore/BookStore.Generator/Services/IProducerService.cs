using BookStore.Application.Contracts.BookAuthors;

namespace BookStore.Generator.Services;

/// <summary>
/// Интерфес службы, занимающейся отправкой сообщений по шине
/// </summary>
public interface IProducerService
{
    /// <summary>
    /// Метод для отправки коллекции контрактов
    /// </summary>
    /// <param name="batch">Коллекция контрактов</param>
    public Task SendAsync(IList<BookAuthorCreateUpdateDto> batch);
}
