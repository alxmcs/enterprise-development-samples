using BookStore.Domain.Model.Books;

namespace BookStore.Domain.Model.Authors;

/// <summary>
/// Доменная служба для имплементации бизнес-логики, связанной с авторами
/// </summary>
/// <param name="authors">Репозиторий авторов</param>
public class AuthorManager(IRepository<Author, int> authors)
{
    /// <summary>
    /// Получает последние 5 книг выбранного автора
    /// </summary>
    /// <param name="authorId">Идентификатор автора</param>
    /// <returns>Список книг</returns>
    public async Task<IList<Book>> GetLast5AuthorsBook(int authorId)
    {
        var author = await authors.Read(authorId);
        return author?.GetLast5AuthorsBook() ?? [];
    }

    /// <summary>
    /// Получает топ 5 авторов по числу написанных страниц
    /// </summary>
    /// <returns>Список кортежей вида (имя автора, число страниц)</returns>
    public async Task<IList<KeyValuePair<string, int?>>> GetTop5AuthorsByPageCount()
    {
        var authorList = await authors.ReadAll();
        return [.. authorList.OrderByDescending(a => a.GetPageCount()).Take(5).Select(a => new KeyValuePair<string, int?>(a.ToString(), a.GetPageCount()))];
    }
}