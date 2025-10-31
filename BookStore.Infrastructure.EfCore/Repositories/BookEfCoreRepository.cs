using BookStore.Domain;
using BookStore.Domain.Model.Books;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.EfCore.Repositories;
public class BookEfCoreRepository(BookStoreDbContext context) : IRepository<Book, int>
{
    private readonly DbSet<Book> _books = context.Books;
    public async Task<Book> Create(Book entity)
    {
        var result = await _books.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(int entityId)
    {
        var entity = await _books.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;
        _books.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Book?> Read(int entityId) =>
        await _books.FirstOrDefaultAsync(e => e.Id == entityId);

    public async Task<IList<Book>> ReadAll() =>
        await _books.ToListAsync();

    public async Task<Book> Update(Book entity)
    {
        _books.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}
