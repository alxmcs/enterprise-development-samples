namespace BookStore.Wasm.Api;

public class BookStoreApiWrapper(IConfiguration configuration)
{
    public readonly BookStoreClient _client = new(configuration["OpenApi:ServerUrl"], new HttpClient());

    public async Task<AuthorDto> CreateAuthor(AuthorCreateUpdateDto newAuhtor) => await _client.AuthorPOSTAsync(newAuhtor);
    public async Task<BookDto> CreateBook(BookCreateUpdateDto newBook) => await _client.BookPOSTAsync(newBook);
    public async Task<BookAuthorDto> CreateBookAuthor(BookAuthorCreateUpdateDto newBookAuhtor) => await _client.BookAuthorPOSTAsync(newBookAuhtor);

    public async Task<AuthorDto> GetAuthor(int id) => await _client.AuthorGETAsync(id);
    public async Task<BookDto> GetBook(int id) => await _client.BookGETAsync(id);
    public async Task<BookAuthorDto> GetBookAuthor(int id) => await _client.BookAuthorGETAsync(id);

    public async Task<AuthorDto> UpdateAuthor(int id, AuthorCreateUpdateDto newAuhtor) => await _client.AuthorPUTAsync(id, newAuhtor);
    public async Task<BookDto> UpdateBook(int id, BookCreateUpdateDto newBook) => await _client.BookPUTAsync(id, newBook);
    public async Task<BookAuthorDto> UpdateBookAuthor(int id, BookAuthorCreateUpdateDto newBookAuhtor) => await _client.BookAuthorPUTAsync(id, newBookAuhtor);

    public async Task DeleteAuthor(int id) => await _client.AuthorDELETEAsync(id);
    public async Task DeleteBook(int id) => await _client.BookDELETEAsync(id);
    public async Task DeleteBookAuthor(int id) => await _client.BookAuthorDELETEAsync(id);

    public async Task<IList<AuthorDto>> GetAllAuthors() => [.. await _client.AuthorAllAsync()];
    public async Task<IList<BookDto>> GetAllBooks() => [.. await _client.BookAllAsync()];
    public async Task<IList<BookAuthorDto>> GetAllBooksAuthors() => [.. await _client.BookAuthorAllAsync()];

    public async Task<Dictionary<string, int?>> GetTop5Authors()
    {
        var dict = new Dictionary<string, int?>();
        foreach (var item in await _client.Top5AuthorsAsync())
            dict.Add(item.Key, item.Value);
        return dict;
    }
    public async Task<IList<BookDto>> GetLast5Books(int id) => [.. await _client.Last5BooksAsync(id)];

    public async Task<IList<AuthorDto>> GetBookAuthors(int bookId)
    {
        var bookAuthors = await GetAllBooksAuthors();
        var authors = new List<AuthorDto>();
        foreach (var ba in bookAuthors)
            if (ba.BookId == bookId)
                authors.Add(await GetAuthor(ba.AuthorId));
        return authors;
    }
    public async Task<IList<BookDto>> GetAuthorBooks(int authorId)
    {
        var bookAuthors = await GetAllBooksAuthors();
        var books = new List<BookDto>();
        foreach (var ba in bookAuthors)
            if (ba.AuthorId == authorId)
                books.Add(await GetBook(ba.BookId));
        return books;
    }
}
