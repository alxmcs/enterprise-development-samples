using BookStore.Domain.Model.BookAuthors;
using BookStore.Domain.Model.Books;
using BookStore.Infrastructure.InMemory;

namespace BookStore.Domain.Model.Authors;

/// <summary>
/// Доменная служба для имплементации бизнес-логики, связанной с авторами
/// </summary>
/// <param name="authors">Репозиторий авторов</param>
/// <param name="bookAuthors">Репозиторий связей</param>
/// <param name="books">Репозиторий книг</param>
public class AuthorManager(IRepository<Author, int> authors, IRepository<BookAuthor, int> bookAuthors, IRepository<Book, int> books)
{
    /// <summary>
    /// Получает последние 5 книг выбранного автора
    /// </summary>
    /// <param name="authorId">Идентификатор автора</param>
    /// <returns>Список книг</returns>
    public List<Book> GetLast5AuthorsBook(int authorId)
    {
        var author = authors.Read(authorId);
        var links = bookAuthors.ReadAll().Where(l => l.AuthorId == author.Id);
        return [.. books.ReadAll()
            .Where(b => links.Any(l => l.BookId == b.Id))
            .OrderByDescending(b => b.Year)
            .Take(5)];
    }

    /// <summary>
    /// Получает топ 5 авторов по числу написанных страниц
    /// </summary>
    /// <returns>Список кортежей вида (имя автора, число страниц)</returns>
    public List<KeyValuePair<string, int?>> GetTop5AuthorsByPageCount()
    {
        var authorList = authors.ReadAll();
        var bookList = books.ReadAll();
        var baList = bookAuthors.ReadAll();
        var query = from author in authorList
                    join ba in baList on author.Id equals ba.AuthorId
                    join book in bookList on ba.BookId equals book.Id
                    select (author, book);
        return [.. query.GroupBy(q => q.author)
            .Select(g => new KeyValuePair<string, int?>(g.Key.ToString(), g.Sum(b => b.book.PageCount)))
            .OrderByDescending( c => c.Value)
            .Take(5)];
    }
}