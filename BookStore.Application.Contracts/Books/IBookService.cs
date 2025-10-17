using BookStore.Application.Contracts.BookAuthors;

namespace BookStore.Application.Contracts.Books;

/// <summary>
/// Наследник аппликейшен службы для книг
/// </summary>
public interface IBookService : IApplicationService<BookDto, BookCreateUpdateDto, int>
{
    /// <summary>
    /// Получает коллекцию связанных сущностей
    /// </summary>
    /// <param name="dtoId">Идентификатор автора</param>
    /// <returns>Список связей</returns>
    List<BookAuthorDto> GetBookAuthors(int dtoId);
}
