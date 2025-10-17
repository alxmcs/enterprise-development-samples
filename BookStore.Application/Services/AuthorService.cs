using AutoMapper;
using BookStore.Application.Contracts.Authors;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Domain.Model.Authors;
using BookStore.Infrastructure.InMemory;

namespace BookStore.Application.Services;

/// <summary>
/// Служба для CRUD-операций над авторами
/// </summary>
/// <param name="authorRepository">Репозиторий авторов</param>
/// <param name="bookAuthorRepository">Репозиторий связей</param>
/// <param name="mapper">Профиль маппинга</param>
public class AuthorService(IRepository<Author, int> authorRepository, IRepository<BookAuthorDto, int> bookAuthorRepository, IMapper mapper) : IAuthorService
{
    /// <inheritdoc/>
    public AuthorDto Create(AuthorCreateUpdateDto dto)
    {
        var newAuthor = mapper.Map<Author>(dto);
        newAuthor.Id = authorRepository.ReadAll().OrderByDescending(a => a.Id).FirstOrDefault(new Author { Id = 1 }).Id + 1;
        authorRepository.Create(newAuthor);
        return mapper.Map<AuthorDto>(newAuthor); 
    }

    /// <inheritdoc/>
    public void Delete(int dtoId)
    {
        authorRepository.Delete(dtoId);
    }

    /// <inheritdoc/>
    public AuthorDto Get(int dtoId) =>  
        mapper.Map<AuthorDto>(authorRepository.Read(dtoId));

    /// <inheritdoc/>
    public List<AuthorDto> GetAll() =>
        mapper.Map<List<AuthorDto>>(authorRepository.ReadAll());

    /// <inheritdoc/>
    public List<BookAuthorDto> GetBookAuthors(int dtoId) =>
        [.. bookAuthorRepository.ReadAll().Where(ba => ba.AuthorId == dtoId)];

    /// <inheritdoc/>
    public AuthorDto Update(AuthorCreateUpdateDto dto, int dtoId)
    {
        var updAuthor = mapper.Map<Author>(dto);
        updAuthor.Id = dtoId;
        authorRepository.Update(updAuthor);
        return mapper.Map<AuthorDto>(updAuthor);
    }
}
