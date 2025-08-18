using AutoMapper;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Domain;
using BookStore.Domain.Model.BookAuthors;

namespace BookStore.Application.Services;
public class BookAuthorService(IRepository<BookAuthor, int> repository, IMapper mapper) : IBookAuthorService
{
    public async Task<BookAuthorDto> Create(BookAuthorCreateUpdateDto dto)
    {
        var newLink = mapper.Map<BookAuthor>(dto);
        var res = await repository.Create(newLink);
        return mapper.Map<BookAuthorDto>(res);
    }

    public async Task<bool> Delete(int dtoId) =>
        await repository.Delete(dtoId);

    public async Task<BookAuthorDto?> Get(int dtoId) =>
        mapper.Map<BookAuthorDto>(await repository.Read(dtoId));

    public async Task<IList<BookAuthorDto>> GetAll() =>
        mapper.Map<List<BookAuthorDto>>(await repository.ReadAll());

    public async Task<BookAuthorDto> Update(BookAuthorCreateUpdateDto dto, int dtoId)
    {
        var updLink = mapper.Map<BookAuthor>(dto);
        var res = await repository.Update(updLink);
        return mapper.Map<BookAuthorDto>(res);
    }

    public async Task ReceiveContractList(IList<BookAuthorCreateUpdateDto> contracts)
    {
        var bookAuthors = mapper.Map<List<BookAuthor>>(contracts);
        foreach (var newLink in bookAuthors)
            await repository.Create(newLink);
    }
}
