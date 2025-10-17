using BookStore.Domain.Data;
using BookStore.Domain.Model.Books;

namespace BookStore.Infrastructure.InMemory;

/// <summary>
/// Имплементация репозитория для книг
/// </summary>
public class BookInMemoryRepository : IRepository<Book, int>
{
    private List<Book> _books;

    /// <inheritdoc/>
    public BookInMemoryRepository()
    {
        _books = DataSeeder.Books;
    }

    /// <inheritdoc/>
    public void Create(Book entity)
    {
        _books.Add(entity);
    }

    /// <inheritdoc/>
    public void Delete(int entityId)
    {
        var author = Read(entityId);
        if (author != null)
            _books.Remove(author);
    }

    /// <inheritdoc/>
    public Book Read(int entityId)
    {
        return _books.First(b => b.Id == entityId);
    }

    /// <inheritdoc/>
    public List<Book> ReadAll()
    {
        return [.. _books];
    }

    /// <inheritdoc/>
    public void Update(Book entity)
    {
        Delete(entity.Id);
        Create(entity);
    }
}
