using BookStore.Domain;
using BookStore.Domain.Model.Authors;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.EfCore.Repositories;
public class AuthorEfCoreRepository(BookStoreDbContext context) : IRepository<Author, int>
{
    private readonly DbSet<Author> _authors = context.Authors;
    public async Task<Author> Create(Author entity)
    {
        var result = await _authors.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(int entityId)
    {
        var entity = await _authors.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;
        _authors.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Author?> Read(int entityId) =>
        await _authors.FirstOrDefaultAsync(e => e.Id == entityId);

    public async Task<IList<Author>> ReadAll() =>
        await _authors.ToListAsync();

    public async Task<Author> Update(Author entity)
    {
        _authors.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}
