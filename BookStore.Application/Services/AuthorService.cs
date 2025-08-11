using AutoMapper;
using BookStore.Application.Contracts.Authors;
using BookStore.Application.Contracts.Books;
using BookStore.Domain.Model.Authors;
using BookStore.Infrastructure.InMemory;

namespace BookStore.Application.Services;
public class AuthorService(IRepository<Author, int> authorRepository, AuthorManager authorManager, IMapper mapper) : IAuthorService
{
    public async Task<AuthorDto> Create(AuthorCreateUpdateDto dto)
    {
        var newAuthor = mapper.Map<Author>(dto);
        var res = await authorRepository.Create(newAuthor);
        return mapper.Map<AuthorDto>(res);
    }

    public async Task<bool> Delete(int dtoId) =>
        await authorRepository.Delete(dtoId);

    public async Task<AuthorDto?> Get(int dtoId) =>
        mapper.Map<AuthorDto>(await authorRepository.Read(dtoId));

    public async Task<IList<AuthorDto>> GetAll() =>
        mapper.Map<List<AuthorDto>>(await authorRepository.ReadAll());

    public async Task<IList<BookDto>> GetLast5AuthorsBook(int dtoId) =>
        mapper.Map<List<BookDto>>(await authorManager.GetLast5AuthorsBook(dtoId));

    public async Task<IList<KeyValuePair<string, int?>>> GetTop5AuthorsByPageCount() =>
        await authorManager.GetTop5AuthorsByPageCount();

    public async Task<AuthorDto> Update(AuthorCreateUpdateDto dto, int dtoId)
    {
        var updAuthor = mapper.Map<Author>(dto);
        var res = await authorRepository.Update(updAuthor);
        return mapper.Map<AuthorDto>(res);
    }
}
