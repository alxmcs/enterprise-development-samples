using BookStore.Domain.Model.Books;

namespace BookStore.Domain.Model.Authors;
/// <summary>
/// Интерфейс для доменной службы
/// </summary>
public  interface IAuthorManager
{
    /// <summary>
    /// Получает последние 5 книг выбранного автора
    /// </summary>
    /// <param name="authorId">Идентификатор автора</param>
    /// <returns>Список книг</returns>
    public Task<IList<Book>> GetLast5AuthorsBook(int authorId);

    /// <summary>
    /// Получает топ 5 авторов по числу написанных страниц
    /// </summary>
    /// <returns>Список кортежей вида (имя автора, число страниц)</returns>
    public Task<IList<KeyValuePair<string, int?>>> GetTop5AuthorsByPageCount();
}
