using AutoMapper;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Application.Contracts.Books;
using BookStore.Domain.Model.Books;
using BookStore.Infrastructure.InMemory;

namespace BookStore.Application.Services;

/// <summary>
/// Служба для CRUD-операций над книгами
/// </summary>
/// <param name="bookRepository">Репозиторий книг</param>
/// <param name="bookAuthorRepository">Репозиторий связей</param>
/// <param name="mapper">Профиль маппинга</param>
public class BookService(IRepository<Book, int> bookRepository, IRepository<BookAuthorDto, int> bookAuthorRepository, IMapper mapper) : IBookService
{
    /// <inheritdoc/>
    public BookDto Create(BookCreateUpdateDto dto)
    {
        var newBook = mapper.Map<Book>(dto);
        newBook.Id = bookRepository.ReadAll().OrderByDescending(a => a.Id).FirstOrDefault(new Book { Id = 1 }).Id + 1;
        bookRepository.Create(newBook);
        return mapper.Map<BookDto>(newBook);
    }

    /// <inheritdoc/>
    public void Delete(int dtoId)
    {
        bookRepository.Delete(dtoId);
    }

    /// <inheritdoc/>
    public BookDto Get(int dtoId) =>
        mapper.Map<BookDto>(bookRepository.Read(dtoId));

    /// <inheritdoc/>
    public List<BookDto> GetAll() =>   
        mapper.Map<List<BookDto>>(bookRepository.ReadAll());

    ///<inheritdoc/>
    public List<BookAuthorDto> GetBookAuthors(int dtoId) =>
        [.. bookAuthorRepository.ReadAll().Where(ba => ba.BookId == dtoId)];

    /// <inheritdoc/>
    public BookDto Update(BookCreateUpdateDto dto, int dtoId)
    {
        var updBook = mapper.Map<Book>(dto);
        updBook.Id = dtoId;
        bookRepository.Update(updBook);
        return mapper.Map<BookDto>(updBook);
    }
}
