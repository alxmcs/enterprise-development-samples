using AutoMapper;
using BookStore.Application.Contracts.Authors;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Domain;
using BookStore.Domain.Model.Authors;
using BookStore.Domain.Model.BookAuthors;

namespace BookStore.Application.Services;

/// <summary>
/// Служба для CRUD-операций над авторами
/// </summary>
/// <param name="authorRepository">Репозиторий авторов</param>
/// <param name="bookAuthorRepository">Репозиторий связей</param>
/// <param name="mapper">Профиль маппинга</param>
public class AuthorService(IRepository<Author, int> authorRepository, IRepository<BookAuthor, int> bookAuthorRepository, IMapper mapper) : IAuthorService
{
    /// <inheritdoc/>
    public async Task<AuthorDto> Create(AuthorCreateUpdateDto dto)
    {
        var newAuthor = mapper.Map<Author>(dto);
        var res = await authorRepository.Create(newAuthor);
        return mapper.Map<AuthorDto>(res);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int dtoId) =>
        await authorRepository.Delete(dtoId);

    /// <inheritdoc/>
    public async Task<AuthorDto?> Get(int dtoId) =>
        mapper.Map<AuthorDto>(await authorRepository.Read(dtoId));

    /// <inheritdoc/>
    public async Task<IList<AuthorDto>> GetAll() =>
        mapper.Map<List<AuthorDto>>(await authorRepository.ReadAll());

    /// <inheritdoc/>
    public async Task<AuthorDto> Update(AuthorCreateUpdateDto dto, int dtoId)
    {
        var updAuthor = mapper.Map<Author>(dto);
        updAuthor.Id = dtoId;
        var res = await authorRepository.Update(updAuthor);
        return mapper.Map<AuthorDto>(res);
    }

    /// <inheritdoc/>
    public async Task<IList<BookAuthorDto>> GetBookAuthors(int dtoId) =>
        mapper.Map<IList<BookAuthorDto>>((await bookAuthorRepository.ReadAll()).Where(ba => ba.AuthorId == dtoId).ToList());
}
