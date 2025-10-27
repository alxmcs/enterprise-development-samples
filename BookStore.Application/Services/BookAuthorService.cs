using AutoMapper;
using BookStore.Application.Contracts;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Domain;
using BookStore.Domain.Model.BookAuthors;

namespace BookStore.Application.Services;

/// <summary>
/// Служба для CRUD-операций над связями книг и авторов
/// </summary>
/// <param name="repository">Репозиторий связей</param>
/// <param name="mapper">Профиль маппинга</param>
public class BookAuthorService(IRepository<BookAuthor, int> repository, IMapper mapper) : IApplicationService<BookAuthorDto, BookAuthorCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public async Task<BookAuthorDto> Create(BookAuthorCreateUpdateDto dto)
    {
        var newLink = mapper.Map<BookAuthor>(dto);
        var res = await repository.Create(newLink);
        return mapper.Map<BookAuthorDto>(res);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int dtoId) =>
        await repository.Delete(dtoId);

    /// <inheritdoc/>
    public async Task<BookAuthorDto?> Get(int dtoId) =>
        mapper.Map<BookAuthorDto>(await repository.Read(dtoId));

    /// <inheritdoc/>
    public async Task<IList<BookAuthorDto>> GetAll() =>
        mapper.Map<List<BookAuthorDto>>(await repository.ReadAll());

    /// <inheritdoc/>
    public async Task<BookAuthorDto> Update(BookAuthorCreateUpdateDto dto, int dtoId)
    {
        var updLink = mapper.Map<BookAuthor>(dto);
        var res = await repository.Update(updLink);
        return mapper.Map<BookAuthorDto>(res);
    }
}
