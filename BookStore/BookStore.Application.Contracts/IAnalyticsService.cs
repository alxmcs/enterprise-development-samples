using BookStore.Application.Contracts.Books;

namespace BookStore.Application.Contracts;
/// <summary>
/// Служба для выполнения аналитических запросов
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получает последние 5 книг выбранного автора
    /// </summary>
    /// <param name="dtoId">Идентификатор автора</param>
    /// <returns>Список книг</returns>
    public Task<IList<BookDto>> GetLast5AuthorsBook(int dtoId);

    /// <summary>
    /// Получает топ 5 авторов по числу написанных страниц
    /// </summary>
    /// <returns>Список кортежей вида (имя автора, число страниц)</returns>
    public Task<IList<KeyValuePair<string, int?>>> GetTop5AuthorsByPageCount();
}
