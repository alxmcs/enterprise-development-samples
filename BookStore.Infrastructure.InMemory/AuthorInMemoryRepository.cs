using BookStore.Domain.Data;
using BookStore.Domain.Model.Authors;

namespace BookStore.Infrastructure.InMemory;

/// <summary>
/// Имплементация репозитория для авторов
/// </summary>
public class AuthorInMemoryRepository : IRepository<Author,int>
{
    private List<Author> _authors;
    public AuthorInMemoryRepository()
    {
        _authors = DataSeeder.Authors;
    }

    public void Create(Author entity)
    {
        _authors.Add(entity);
    }

    public void Delete(int entityId)
    {
        var author = Read(entityId);
        if (author != null)
            _authors.Remove(author);
    }

    public Author Read(int entityId)
    {
        return _authors.First(a => a.Id == entityId);
    }

    public List<Author> ReadAll()
    {
        return [.. _authors];
    }

    public void Update(Author entity)
    {
        Delete(entity.Id);
        Create(entity);
    }
}
