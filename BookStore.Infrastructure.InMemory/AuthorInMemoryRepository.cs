using BookStore.Domain.Data;
using BookStore.Domain.Model.Authors;

namespace BookStore.Infrastructure.InMemory;

/// <summary>
/// Имплементация репозитория для авторов
/// </summary>
public class AuthorInMemoryRepository : IRepository<Author, int>
{
    private readonly List<Author> _authors;

    /// <inheritdoc/>
    public AuthorInMemoryRepository()
    {
        _authors = DataSeeder.Authors;
    }

    /// <inheritdoc/>
    public void Create(Author entity)
    {
        _authors.Add(entity);
    }

    /// <inheritdoc/>
    public void Delete(int entityId)
    {
        var author = Read(entityId);
        if (author != null)
            _authors.Remove(author);
    }

    /// <inheritdoc/>
    public Author Read(int entityId)
    {
        return _authors.First(a => a.Id == entityId);
    }

    /// <inheritdoc/>
    public List<Author> ReadAll()
    {
        return [.. _authors];
    }

    /// <inheritdoc/>
    public void Update(Author entity)
    {
        Delete(entity.Id);
        Create(entity);
    }
}
