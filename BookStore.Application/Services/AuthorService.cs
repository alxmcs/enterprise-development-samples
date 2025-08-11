using AutoMapper;
using BookStore.Application.Contracts.Authors;
using BookStore.Application.Contracts.Books;
using BookStore.Domain.Model.Authors;
using BookStore.Infrastructure.InMemory;

namespace BookStore.Application.Services;
public class AuthorService(IRepository<Author, int> authorRepository, AuthorManager authorManager, IMapper mapper) : IAuthorService
{
    public AuthorDto Create(AuthorCreateUpdateDto dto)
    {
        var newAuthor = mapper.Map<Author>(dto);
        newAuthor.Id = authorRepository.ReadAll().OrderByDescending(a => a.Id).FirstOrDefault(new Author { Id = 1 }).Id + 1;
        authorRepository.Create(newAuthor);
        return mapper.Map<AuthorDto>(newAuthor); 
    }

    public void Delete(int dtoId)
    {
        authorRepository.Delete(dtoId);
    }

    public AuthorDto Get(int dtoId) =>  
        mapper.Map<AuthorDto>(authorRepository.Read(dtoId));
    
    public List<AuthorDto> GetAll() =>
        mapper.Map<List<AuthorDto>>(authorRepository.ReadAll());
    
    public List<BookDto> GetLast5AuthorsBook(int dtoId) =>   
        mapper.Map<List<BookDto>>(authorManager.GetLast5AuthorsBook(dtoId));

    public List<KeyValuePair<string, int?>> GetTop5AuthorsByPageCount() =>
        authorManager.GetTop5AuthorsByPageCount();
    
    public AuthorDto Update(AuthorCreateUpdateDto dto, int dtoId)
    {
        var updAuthor = mapper.Map<Author>(dto);
        updAuthor.Id = dtoId;
        authorRepository.Update(updAuthor);
        return mapper.Map<AuthorDto>(updAuthor);
    }
}
