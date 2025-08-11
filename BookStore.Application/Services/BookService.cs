using AutoMapper;
using BookStore.Application.Contracts;
using BookStore.Application.Contracts.Books;
using BookStore.Domain.Model.Books;
using BookStore.Infrastructure.InMemory;

namespace BookStore.Application.Services;
public class BookService(IRepository<Book, int> repository, IMapper mapper) : IApplicationService<BookDto, BookCreateUpdateDto, int>
{
    public async Task<BookDto> Create(BookCreateUpdateDto dto)
    {
        var newBook = mapper.Map<Book>(dto);
        var res = await repository.Create(newBook);
        return mapper.Map<BookDto>(res);
    }

    public async Task<bool> Delete(int dtoId) =>
        await repository.Delete(dtoId);


    public async Task<BookDto?> Get(int dtoId) =>
        mapper.Map<BookDto>(await repository.Read(dtoId));

    public async Task<IList<BookDto>> GetAll() =>
        mapper.Map<List<BookDto>>(await repository.ReadAll());

    public async Task<BookDto> Update(BookCreateUpdateDto dto, int dtoId)
    {
        var updBook = mapper.Map<Book>(dto);
        var res = await repository.Update(updBook);
        return mapper.Map<BookDto>(res);
    }
}
