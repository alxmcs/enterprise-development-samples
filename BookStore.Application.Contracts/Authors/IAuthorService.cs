using BookStore.Application.Contracts.Books;

namespace BookStore.Application.Contracts.Authors;

/// <summary>
/// Наследник аппликейшен службы для авторов
/// </summary>
public interface IAuthorService : IApplicationService<AuthorDto, AuthorCreateUpdateDto, int>
{
    /// <summary>
    /// Получает последние 5 книг выбранного автора
    /// </summary>
    /// <param name="dtoId">Идентификатор автора</param>
    /// <returns>Список книг</returns>
    Task<IList<BookDto>> GetLast5AuthorsBook(int dtoId);

    /// <summary>
    /// Получает топ 5 авторов по числу написанных страниц
    /// </summary>
    /// <returns>Список кортежей вида (имя автора, число страниц)</returns>
    Task<IList<KeyValuePair<string, int?>>> GetTop5AuthorsByPageCount();
}
