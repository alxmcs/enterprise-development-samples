using BookStore.Domain;
using BookStore.Domain.Model.Authors;

namespace BookStore.Infrastructure.InMemory;

/// <summary>
/// Имплементация инмемори репозитория для авторов
/// </summary>
public class AuthorInMemoryRepository(List<Author> authors) : IRepository<Author, int>
{
    /// <inheritdoc/>
    public Task<Author> Create(Author entity)
    {
        authors.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc/>
    public Task<bool> Delete(int entityId)
    {
        var author = Read(entityId).Result;
        if (author == null)
            return Task.FromResult(false);
        var res = authors.Remove(author);
        return Task.FromResult(res);
    }

    /// <inheritdoc/>
    public Task<Author?> Read(int entityId) =>
        Task.FromResult(authors.FirstOrDefault(a => a.Id == entityId));

    /// <inheritdoc/>
    public Task<IList<Author>> ReadAll() =>
        Task.FromResult<IList<Author>>([.. authors]);

    /// <inheritdoc/>
    public Task<Author> Update(Author entity)
    {
        Delete(entity.Id);
        Create(entity);
        return Task.FromResult(entity);
    }
}
