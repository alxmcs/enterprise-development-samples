namespace BookStore.Domain.Tets;

/// <summary>
/// Фикстура для того, чтобы расшарить контекст с доменной службой между всеми юнит-тестами
/// </summary>
public class AuthorManagerFixture
{
    private readonly AuthorInMemoryRepository _authorRepository;


    public AuthorManager AuthorManager { get; init; }

    public AuthorManagerFixture()
    {
        var authors = DataSeeder.Authors;
        var bookAuthors = DataSeeder.BookAuthors;
        var books = DataSeeder.Books;
        foreach (var ba in bookAuthors)
        {
            ba.Author = authors.FirstOrDefault(a => a.Id == ba.AuthorId);
            ba.Book = books.FirstOrDefault(a => a.Id == ba.BookId);
        }
        foreach (var b in books)
            b.BookAuthors = [.. bookAuthors.Where(ba => ba.BookId == b.Id)];
        foreach (var a in authors)
            a.BookAuthors = [.. bookAuthors.Where(ba => ba.AuthorId == a.Id)];

        _authorRepository = new(authors);

        AuthorManager = new(_authorRepository);
    }
}
