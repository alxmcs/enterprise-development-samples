using AutoMapper;
using BookStore.Contracts.Author;
using BookStore.Contracts.Book;
using BookStore.Contracts.BookAuthor;
using BookStore.Contracts.Protos;
using static BookStore.Contracts.Protos.AuthorService;
using static BookStore.Contracts.Protos.BookAuthorService;
using static BookStore.Contracts.Protos.BookService;

namespace BookStore.Grpc.Client.Grpc;

public class BookStoreGrpcWrapper(BookServiceClient bookClient, AuthorServiceClient authorClient, BookAuthorServiceClient bookAuthorClient, IMapper mapper) : IBookStoreWrapper
{
    public async Task<AuthorDto> CreateAuthor(AuthorCreateUpdateDto newAuhtor) => mapper.Map<AuthorDto>(await authorClient.CreateAsync(mapper.Map<AuthorCreateRequest>(newAuhtor)));
    public async Task<BookDto> CreateBook(BookCreateUpdateDto newBook) => mapper.Map<BookDto>(await bookClient.CreateAsync(mapper.Map<BookCreateRequest>(newBook)));
    public async Task<BookAuthorDto> CreateBookAuthor(BookAuthorCreateUpdateDto newBookAuhtor) => mapper.Map<BookAuthorDto>(await bookAuthorClient.CreateAsync(mapper.Map<BookAuthorCreateRequest>(newBookAuhtor)));

    public async Task<AuthorDto> UpdateAuthor(int id, AuthorCreateUpdateDto newAuhtor) => mapper.Map<AuthorDto>(await authorClient.UpdateAsync(new() { Id = id, Author = mapper.Map<AuthorCreateRequest>(newAuhtor) }));
    public async Task<BookDto> UpdateBook(int id, BookCreateUpdateDto newBook) => mapper.Map<BookDto>(await bookClient.UpdateAsync(new() { Id = id, Book = mapper.Map<BookCreateRequest>(newBook) }));
    public async Task<BookAuthorDto> UpdateBookAuthor(int id, BookAuthorCreateUpdateDto newBookAuhtor) => mapper.Map<BookAuthorDto>(await bookAuthorClient.UpdateAsync(new() { Id = id, BookAuthor = mapper.Map<BookAuthorCreateRequest>(newBookAuhtor) }));

    public async Task DeleteAuthor(int id) => await authorClient.DeleteAsync(new() { Value = id });
    public async Task DeleteBook(int id) => await bookClient.DeleteAsync(new() { Value = id });
    public async Task DeleteBookAuthor(int id) => await bookAuthorClient.DeleteAsync(new() { Value = id });

    public async Task<AuthorDto> GetAuthor(int id) => mapper.Map<AuthorDto>(await authorClient.GetByIdAsync(new() { Value = id }));
    public async Task<BookDto> GetBook(int id) => mapper.Map<BookDto>(await bookClient.GetByIdAsync(new() { Value = id }));
    public async Task<BookAuthorDto> GetBookAuthor(int id) => mapper.Map<BookAuthorDto>(await bookAuthorClient.GetByIdAsync(new() { Value = id }));

    public async Task<IList<AuthorDto>> GetAllAuthors() => [.. mapper.Map<IList<AuthorDto>>((await authorClient.GetListAsync(new())).Authors.ToList())];
    public async Task<IList<BookDto>> GetAllBooks() => [.. mapper.Map<IList<BookDto>>((await bookClient.GetListAsync(new())).Books.ToList())];
    public async Task<IList<BookAuthorDto>> GetAllBooksAuthors() => [.. mapper.Map<IList<BookAuthorDto>>((await bookAuthorClient.GetListAsync(new())).BookAuthors.ToList())];

    public async Task<IList<AuthorDto>> GetBookAuthors(int bookId) => [.. mapper.Map<IList<AuthorDto>>((await authorClient.GetBookAuthorsAsync(new() { Value = bookId })).Authors.ToList())];
    public async Task<IList<BookDto>> GetAuthorBooks(int authorId) => [.. mapper.Map<IList<BookDto>>((await bookClient.GetAuthorBooksAsync(new() { Value = authorId })).Books.ToList())];
}
