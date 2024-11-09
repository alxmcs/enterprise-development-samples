namespace BookStore.Contracts.Book;

/// <summary>
/// Наследник CRUD-службы с дополнительными операциями для книг
/// </summary>
public interface IBookService : ICrudService<BookDto, BookCreateUpdateDto, int>
{
    /// <summary>
    /// Возвращает авторов издания
    /// </summary>
    /// <param name="authorId">Идентификатор автора</param>
    /// <returns>Список изданий</returns>
    public Task<List<BookDto>?> GetAuthorBooks(int authorId);
}
