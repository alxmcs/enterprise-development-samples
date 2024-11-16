using AutoMapper;
using BookStore.EfCore;
using BookStore.Contracts.Author;
using BookStore.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Services;
public class AuthorCrudService(IMapper mapper, BookStoreDbContext dbContext) : IAuthorService
{
    public async Task<AuthorDto> Create(AuthorCreateUpdateDto newDto, CancellationToken cancellationToken)
    {
        var newAuthor = mapper.Map<Author>(newDto);
        var entry = await dbContext.Authors!.AddAsync(newAuthor, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return mapper.Map<AuthorDto>(entry.Entity);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var entry = await dbContext.Authors!.FindAsync([id, cancellationToken], cancellationToken: cancellationToken);
        if (entry == null)
            return false;
        dbContext.Authors.Remove(entry);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IList<AuthorDto>?> GetBookAuthors(int bookId, CancellationToken cancellationToken)
    {
        var entry = await dbContext.Books!.FindAsync([bookId, cancellationToken], cancellationToken: cancellationToken);
        if (entry == null)
            return null;
        var authorIds = entry.BookAuthors?.Select(x => x.AuthorId);
        if (authorIds == null)
            return null;
        var authors = await dbContext.Authors!.Where(x => authorIds.Contains(x.Id)).ToListAsync(cancellationToken);
        return mapper.Map<List<AuthorDto>>(authors);
    }

    public async Task<AuthorDto?> GetById(int id, CancellationToken cancellationToken) =>
        mapper.Map<AuthorDto>(await dbContext.Authors!.FindAsync([id, cancellationToken], cancellationToken: cancellationToken));

    public async Task<IList<AuthorDto>> GetList(CancellationToken cancellationToken) =>
        mapper.Map<List<AuthorDto>>(await dbContext.Authors!.ToListAsync(cancellationToken));

    public async Task<AuthorDto> Update(int key, AuthorCreateUpdateDto newDto, CancellationToken cancellationToken)
    {
        var entry = await dbContext.Authors!.FindAsync([key, cancellationToken], cancellationToken: cancellationToken);
        if (entry == null) return null!;
        var res = mapper.Map(newDto, entry);
        await dbContext.SaveChangesAsync(cancellationToken);
        return mapper.Map<AuthorDto>(res);
    }
}
