using AutoMapper;
using BookStore.Contracts;
using BookStore.EfCore;
using BookStore.Contracts.BookAuthor;
using BookStore.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Services;
public class BookAuthorCrudService(IMapper mapper, BookStoreDbContext dbContext) : ICrudService<BookAuthorDto, BookAuthorCreateUpdateDto, int>
{
    public async Task<BookAuthorDto> Create(BookAuthorCreateUpdateDto newDto)
    {
        var newAuthor = mapper.Map<BookAuthor>(newDto);
        var entry = await dbContext.BookAuthors!.AddAsync(newAuthor);
        await dbContext.SaveChangesAsync();
        return mapper.Map<BookAuthorDto>(entry.Entity);
    }

    public async Task<bool> Delete(int key)
    {
        var entry = await dbContext.BookAuthors!.FindAsync(key);
        if (entry == null)
            return false;
        dbContext.BookAuthors.Remove(entry);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<BookAuthorDto> GetById(int id) =>
        mapper.Map<BookAuthorDto>(await dbContext.BookAuthors!.FindAsync(id));

    public async Task<IList<BookAuthorDto>> GetList() =>
        mapper.Map<List<BookAuthorDto>>(await dbContext.BookAuthors!.ToListAsync());

    public async Task<BookAuthorDto> Update(int key, BookAuthorCreateUpdateDto newDto)
    {
        var entry = await dbContext.BookAuthors!.FindAsync(key);
        if (entry == null) return null!;
        var res = mapper.Map(newDto, entry);
        await dbContext.SaveChangesAsync();
        return mapper.Map<BookAuthorDto>(res);
    }
}
