using BookStore.Domain.Data;
using BookStore.Domain.Model.BookAuthors;

namespace BookStore.Infrastructure.InMemory;

/// <summary>
/// Имплементация репозитория для связи авторов и книг
/// </summary>
public class BookAuthorInMemoryRepository : IRepository<BookAuthor, int>
{
    private readonly List<BookAuthor> _bookAuthors;

    /// <inheritdoc/>
    public BookAuthorInMemoryRepository()
    {
        _bookAuthors = DataSeeder.BookAuthors;
    }

    /// <inheritdoc/>
    public void Create(BookAuthor entity)
    {
        _bookAuthors.Add(entity);
    }

    /// <inheritdoc/>
    public void Delete(int entityId)
    {
        var author = Read(entityId);
        if (author != null)
            _bookAuthors.Remove(author);
    }

    /// <inheritdoc/>
    public BookAuthor Read(int entityId)
    {
        return _bookAuthors.First(ba => ba.Id == entityId);
    }

    /// <inheritdoc/>
    public List<BookAuthor> ReadAll()
    {
        return [.. _bookAuthors];
    }

    /// <inheritdoc/>
    public void Update(BookAuthor entity)
    {
        Delete(entity.Id);
        Create(entity);
    }
}