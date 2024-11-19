using AutoMapper;
using BookStore.Contracts.Book;
using BookStore.Domain.Model;
using BookStore.EfCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Services;
public class BookCrudService(IMapper mapper, BookStoreDbContext dbContext) : IBookService
{
    public async Task<BookDto> Create(BookCreateUpdateDto newDto, CancellationToken cancellationToken)
    {
        var newBook = mapper.Map<Book>(newDto);
        var entry = await dbContext.Books!.AddAsync(newBook, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return mapper.Map<BookDto>(entry.Entity);
    }

    public async Task<bool> Delete(int key, CancellationToken cancellationToken)
    {
        var entry = await dbContext.Books!.FindAsync([key, cancellationToken], cancellationToken: cancellationToken);
        if (entry == null)
            return false;
        dbContext.Books.Remove(entry);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<BookDto>?> GetAuthorBooks(int authorId, CancellationToken cancellationToken)
    {
        var entry = await dbContext.Authors!.FindAsync([authorId, cancellationToken], cancellationToken: cancellationToken);
        if (entry == null)
            return null;
        var bookId = entry.BookAuthors?.Select(x => x.BookId);
        if (bookId == null)
            return null;
        var books = await dbContext.Books!.Where(x => bookId.Contains(x.Id)).ToListAsync(cancellationToken);
        return mapper.Map<List<BookDto>>(books);
    }

    public async Task<BookDto?> GetById(int id, CancellationToken cancellationToken) =>
        mapper.Map<BookDto>(await dbContext.Books!.FindAsync([id, cancellationToken], cancellationToken: cancellationToken));
    public async Task<IList<BookDto>> GetList(CancellationToken cancellationToken) =>
        mapper.Map<List<BookDto>>(await dbContext.Books!.ToListAsync(cancellationToken));

    public async Task<BookDto> Update(int key, BookCreateUpdateDto newDto, CancellationToken cancellationToken)
    {
        var entry = await dbContext.Books!.FindAsync([key, cancellationToken], cancellationToken: cancellationToken);
        if (entry == null) return null!;
        var res = mapper.Map(newDto, entry);
        await dbContext.SaveChangesAsync(cancellationToken);
        return mapper.Map<BookDto>(res);
    }
}
