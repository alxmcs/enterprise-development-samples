using AutoMapper;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Application.Contracts.Books;
using BookStore.Domain.Model.BookAuthors;
using BookStore.Domain;
using BookStore.Domain.Model.Books;

namespace BookStore.Application.Services;

/// <summary>
/// Служба для CRUD-операций над книгами
/// </summary>
/// <param name="repository">Репозиторий книг</param>
/// <param name="bookAuthorRepository">Репозиторий связей</param>
/// <param name="mapper">Профиль маппинга</param>
public class BookService(IRepository<Book, int> repository, IRepository<BookAuthor, int> bookAuthorRepository, IMapper mapper) : IBookService
{
    /// <inheritdoc/>
    public async Task<BookDto> Create(BookCreateUpdateDto dto)
    {
        var newBook = mapper.Map<Book>(dto);
        var res = await repository.Create(newBook);
        return mapper.Map<BookDto>(res);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int dtoId) =>
        await repository.Delete(dtoId);


    /// <inheritdoc/>
    public async Task<BookDto?> Get(int dtoId) =>
        mapper.Map<BookDto>(await repository.Read(dtoId));

    /// <inheritdoc/>
    public async Task<IList<BookDto>> GetAll() =>
        mapper.Map<List<BookDto>>(await repository.ReadAll());

    /// <inheritdoc/>
    public async Task<BookDto> Update(BookCreateUpdateDto dto, int dtoId)
    {
        var updBook = mapper.Map<Book>(dto);
        updBook.Id = dtoId;
        var res = await repository.Update(updBook);
        return mapper.Map<BookDto>(res);
    }

    /// <inheritdoc/>
    public async Task<IList<BookAuthorDto>> GetBookAuthors(int dtoId) =>
        mapper.Map<IList<BookAuthorDto>>((await bookAuthorRepository.ReadAll()).Where(ba => ba.BookId == dtoId).ToList());
}
