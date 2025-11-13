using BookStore.Domain;
using BookStore.Domain.Model.BookAuthors;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.EfCore.Repositories;
public class BookAuthorEfCoreRepository(BookStoreDbContext context) : IRepository<BookAuthor, int>
{
    private readonly DbSet<BookAuthor> _bookAuthors = context.BookAuthors;

    public async Task<BookAuthor> Create(BookAuthor entity)
    {
        var result = await _bookAuthors.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(int entityId)
    {
        var entity = await _bookAuthors.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;
        _bookAuthors.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<BookAuthor?> Read(int entityId) =>
        await _bookAuthors.FirstOrDefaultAsync(e => e.Id == entityId);

    public async Task<IList<BookAuthor>> ReadAll() =>
        await _bookAuthors.ToListAsync();

    public async Task<BookAuthor> Update(BookAuthor entity)
    {
        _bookAuthors.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}
