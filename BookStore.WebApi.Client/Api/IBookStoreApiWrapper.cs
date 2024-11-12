
namespace BookStore.WebApi.Client.Api;

public interface IBookStoreApiWrapper
{
    Task<AuthorDto> CreateAuthor(AuthorCreateUpdateDto newAuhtor);
    Task<BookDto> CreateBook(BookCreateUpdateDto newBook);
    Task<BookAuthorDto> CreateBookAuthor(BookAuthorCreateUpdateDto newBookAuhtor);
    Task DeleteAuthor(int id);
    Task DeleteBook(int id);
    Task DeleteBookAuthor(int id);
    Task<IList<AuthorDto>> GetAllAuthors();
    Task<IList<BookDto>> GetAllBooks();
    Task<IList<BookAuthorDto>> GetAllBooksAuthors();
    Task<AuthorDto> GetAuthor(int id);
    Task<IList<BookDto>> GetAuthorBooks(int authorId);
    Task<BookDto> GetBook(int id);
    Task<BookAuthorDto> GetBookAuthor(int id);
    Task<IList<AuthorDto>> GetBookAuthors(int bookId);
    Task<AuthorDto> UpdateAuthor(int id, AuthorCreateUpdateDto newAuhtor);
    Task<BookDto> UpdateBook(int id, BookCreateUpdateDto newBook);
    Task<BookAuthorDto> UpdateBookAuthor(int id, BookAuthorCreateUpdateDto newBookAuhtor);
}