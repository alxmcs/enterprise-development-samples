using AutoMapper;
using BookStore.Application.Contracts;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Domain.Model.BookAuthors;
using BookStore.Infrastructure.InMemory;

namespace BookStore.Application.Services;

/// <summary>
/// Служба для CRUD-операций над связями книг и авторов
/// </summary>
/// <param name="repository">Репозиторий связей</param>
/// <param name="mapper">Профиль маппинга</param>
public class BookAuthorService(IRepository<BookAuthor, int> repository, IMapper mapper) : IApplicationService<BookAuthorDto, BookAuthorCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public BookAuthorDto Create(BookAuthorCreateUpdateDto dto)
    {
        var newLink = mapper.Map<BookAuthor>(dto);
        newLink.Id = repository.ReadAll().OrderByDescending(a => a.Id).FirstOrDefault(new BookAuthor { Id = 1, AuthorId = 0, BookId = 0 }).Id + 1;
        repository.Create(newLink);
        return mapper.Map<BookAuthorDto>(newLink);
    }

    /// <inheritdoc/>
    public void Delete(int dtoId)
    {
        repository.Delete(dtoId);
    }

    /// <inheritdoc/>
    public BookAuthorDto Get(int dtoId) =>
        mapper.Map<BookAuthorDto>(repository.Read(dtoId));

    /// <inheritdoc/>
    public List<BookAuthorDto> GetAll() =>
        mapper.Map<List<BookAuthorDto>>(repository.ReadAll());

    /// <inheritdoc/>
    public BookAuthorDto Update(BookAuthorCreateUpdateDto dto, int dtoId)
    {
        var updLink = mapper.Map<BookAuthor>(dto);
        updLink.Id = dtoId;
        repository.Update(updLink);
        return mapper.Map<BookAuthorDto>(updLink);
    }
}
