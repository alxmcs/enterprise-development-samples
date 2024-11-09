using AutoMapper;
using BookStore.Contracts;
using BookStore.Contracts.Book;
using BookStore.Domain.Model;
using BookStore.EfCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Services;
public class BookCrudService(IMapper mapper, BookStoreDbContext dbContext) : ICrudService<BookDto, BookCreateUpdateDto, int>
{
    public async Task<BookDto> Create(BookCreateUpdateDto newDto)
    {
        var newBook = mapper.Map<Book>(newDto);
        var entry = await dbContext.Books!.AddAsync(newBook);
        await dbContext.SaveChangesAsync();
        return mapper.Map<BookDto>(entry.Entity);
    }

    public async Task<bool> Delete(int key)
    {
        var entry = await dbContext.Books!.FindAsync(key);
        if (entry == null)
            return false;
        dbContext.Books.Remove(entry);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<BookDto> GetById(int id) =>
        mapper.Map<BookDto>(await dbContext.Books!.FindAsync(id));
    public async Task<IList<BookDto>> GetList() =>
        mapper.Map<List<BookDto>>(await dbContext.Books!.ToListAsync());
    
    public async Task<BookDto> Update(int key, BookCreateUpdateDto newDto)
    {
        var entry = await dbContext.Books!.FindAsync(key);
        if (entry == null) return null!;
        var res = mapper.Map(newDto, entry);
        await dbContext.SaveChangesAsync();
        return mapper.Map<BookDto>(res);
    }

    Task<BookDto> ICrudService<BookDto, BookCreateUpdateDto, int>.Create(BookCreateUpdateDto newDto)
    {
        throw new NotImplementedException();
    }
}
