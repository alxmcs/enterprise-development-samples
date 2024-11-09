using AutoMapper;
using BookStore.Contracts;
using BookStore.EfCore;
using BookStore.Contracts.Author;
using BookStore.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Services;
public class AuthorCrudService(IMapper mapper, BookStoreDbContext dbContext) : ICrudService<AuthorDto, AuthorCreateUpdateDto, int>
{
    public async Task<AuthorDto> Create(AuthorCreateUpdateDto newDto)
    {
        var newAuthor = mapper.Map<Author>(newDto);
        var entry = await dbContext.Authors!.AddAsync(newAuthor);
        await dbContext.SaveChangesAsync();
        return mapper.Map <AuthorDto>(entry.Entity);
    }

    public async Task<bool> Delete(int id)
    {
        var entry = await dbContext.Authors!.FindAsync(id);
        if (entry == null)
            return false;
        dbContext.Authors.Remove(entry);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<AuthorDto> GetById(int id) =>
        mapper.Map<AuthorDto>(await dbContext.Authors!.FindAsync(id));

    public async Task<IList<AuthorDto>> GetList() =>
        mapper.Map<List<AuthorDto>>(await dbContext.Authors!.ToListAsync());

    public async Task<AuthorDto> Update(int key, AuthorCreateUpdateDto newDto)
    {
        var entry = await dbContext.Authors!.FindAsync(key);
        if (entry == null) return null!;
        var res = mapper.Map(newDto, entry);
        await dbContext.SaveChangesAsync();
        return mapper.Map<AuthorDto>(res);
    }
}
