using AutoMapper;
using BookStore.Application.Contracts;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Domain.Model.BookAuthors;
using BookStore.Infrastructure.InMemory;

namespace BookStore.Application.Services;
public class BookAuthorService(IRepository<BookAuthor, int> repository, IMapper mapper) : IApplicationService<BookAuthorDto, BookAuthorCreateUpdateDto, int>
{
    public BookAuthorDto Create(BookAuthorCreateUpdateDto dto)
    {
        var newLink = mapper.Map<BookAuthor>(dto);
        newLink.Id = repository.ReadAll().OrderByDescending(a => a.Id).FirstOrDefault(new BookAuthor { Id = 1, AuthorId = 0, BookId = 0 }).Id + 1;
        repository.Create(newLink);
        return mapper.Map<BookAuthorDto>(newLink);
    }

    public void Delete(int dtoId)
    {
        repository.Delete(dtoId);
    }

    public BookAuthorDto Get(int dtoId) =>
        mapper.Map<BookAuthorDto>(repository.Read(dtoId));

    public List<BookAuthorDto> GetAll() =>
        mapper.Map<List<BookAuthorDto>>(repository.ReadAll());

    public BookAuthorDto Update(BookAuthorCreateUpdateDto dto, int dtoId)
    {
        var updLink = mapper.Map<BookAuthor>(dto);
        updLink.Id = dtoId;
        repository.Update(updLink);
        return mapper.Map<BookAuthorDto>(updLink);
    }
}
