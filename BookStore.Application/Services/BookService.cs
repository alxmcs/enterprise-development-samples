using AutoMapper;
using BookStore.Application.Contracts;
using BookStore.Application.Contracts.Books;
using BookStore.Domain.Model.Books;
using BookStore.Infrastructure.InMemory;

namespace BookStore.Application.Services;
public class BookService(IRepository<Book, int> repository, IMapper mapper) : IApplicationService<BookDto, BookCreateUpdateDto, int>
{
    public BookDto Create(BookCreateUpdateDto dto)
    {
        var newBook = mapper.Map<Book>(dto);
        newBook.Id = repository.ReadAll().OrderByDescending(a => a.Id).FirstOrDefault(new Book { Id = 1 }).Id + 1;
        repository.Create(newBook);
        return mapper.Map<BookDto>(newBook);
    }

    public void Delete(int dtoId)
    {
        repository.Delete(dtoId);
    }

    public BookDto Get(int dtoId) =>
        mapper.Map<BookDto>(repository.Read(dtoId));

    public List<BookDto> GetAll() =>   
        mapper.Map<List<BookDto>>(repository.ReadAll());
    
    public BookDto Update(BookCreateUpdateDto dto, int dtoId)
    {
        var updBook = mapper.Map<Book>(dto);
        updBook.Id = dtoId;
        repository.Update(updBook);
        return mapper.Map<BookDto>(updBook);
    }
}
