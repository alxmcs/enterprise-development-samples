using BookStore.Domain;
using BookStore.Domain.Model.Books;

namespace BookStore.Infrastructure.InMemory;

/// <summary>
/// Имплементация инмемори репозитория для книг
/// </summary>
public class BookInMemoryRepository(List<Book> books) : IRepository<Book, int>
{
    /// <inheritdoc/>
    public Task<Book> Create(Book entity)
    {
        books.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc/>
    public Task<bool> Delete(int entityId)
    {
        var book = Read(entityId).Result;
        if (book == null)
            return Task.FromResult(false);
        var res = books.Remove(book);
        return Task.FromResult(res);
    }

    /// <inheritdoc/>
    public Task<Book?> Read(int entityId) =>
        Task.FromResult(books.FirstOrDefault(a => a.Id == entityId));

    /// <inheritdoc/>
    public Task<IList<Book>> ReadAll() =>
        Task.FromResult<IList<Book>>([.. books]);

    /// <inheritdoc/>
    public Task<Book> Update(Book entity)
    {
        Delete(entity.Id);
        Create(entity);
        return Task.FromResult(entity);
    }
}
