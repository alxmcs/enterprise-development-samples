using BookStore.Domain;
using BookStore.Domain.Model.BookAuthors;

namespace BookStore.Infrastructure.InMemory;

/// <summary>
/// Имплементация инмемори репозитория для связи авторов и книг
/// </summary>
public class BookAuthorInMemoryRepository(List<BookAuthor> bookAuthors) : IRepository<BookAuthor, int>
{
    /// <inheritdoc/>
    public Task<BookAuthor> Create(BookAuthor entity)
    {
        bookAuthors.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc/>
    public Task<bool> Delete(int entityId)
    {
        var author = Read(entityId).Result;
        if (author == null)
            return Task.FromResult(false);
        var res = bookAuthors.Remove(author);
        return Task.FromResult(res);
    }

    /// <inheritdoc/>
    public Task<BookAuthor?> Read(int entityId) =>
        Task.FromResult(bookAuthors.FirstOrDefault(ba => ba.Id == entityId));

    /// <inheritdoc/>
    public Task<IList<BookAuthor>> ReadAll() =>
        Task.FromResult<IList<BookAuthor>>([.. bookAuthors]);

    /// <inheritdoc/>
    public Task<BookAuthor> Update(BookAuthor entity)
    {
        Delete(entity.Id);
        Create(entity);
        return Task.FromResult(entity);
    }
}