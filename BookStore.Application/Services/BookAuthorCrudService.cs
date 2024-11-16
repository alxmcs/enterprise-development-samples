using AutoMapper;
using BookStore.Contracts;
using BookStore.EfCore;
using BookStore.Contracts.BookAuthor;
using BookStore.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Services;
public class BookAuthorCrudService(IMapper mapper, BookStoreDbContext dbContext) : ICrudService<BookAuthorDto, BookAuthorCreateUpdateDto, int>
{
    public async Task<BookAuthorDto> Create(BookAuthorCreateUpdateDto newDto, CancellationToken cancellationToken)
    {
        var newAuthor = mapper.Map<BookAuthor>(newDto);
        var entry = await dbContext.BookAuthors!.AddAsync(newAuthor, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return mapper.Map<BookAuthorDto>(entry.Entity);
    }

    public async Task<bool> Delete(int key, CancellationToken cancellationToken)
    {
        var entry = await dbContext.BookAuthors!.FindAsync([key, cancellationToken], cancellationToken: cancellationToken);
        if (entry == null)
            return false;
        dbContext.BookAuthors.Remove(entry);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<BookAuthorDto?> GetById(int id, CancellationToken cancellationToken) =>
        mapper.Map<BookAuthorDto>(await dbContext.BookAuthors!.FindAsync([id, cancellationToken], cancellationToken: cancellationToken));

    public async Task<IList<BookAuthorDto>> GetList(CancellationToken cancellationToken) =>
        mapper.Map<List<BookAuthorDto>>(await dbContext.BookAuthors!.ToListAsync(cancellationToken));

    public async Task<BookAuthorDto> Update(int key, BookAuthorCreateUpdateDto newDto, CancellationToken cancellationToken)
    {
        var entry = await dbContext.BookAuthors!.FindAsync([key, cancellationToken], cancellationToken: cancellationToken);
        if (entry == null) return null!;
        var res = mapper.Map(newDto, entry);
        await dbContext.SaveChangesAsync(cancellationToken);
        return mapper.Map<BookAuthorDto>(res);
    }
}
