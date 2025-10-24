using BookStore.Application.Contracts.BookAuthors;

namespace BookStore.Application.Contracts.Authors;

/// <summary>
/// Наследник аппликейшен службы для авторов
/// </summary>
public interface IAuthorService : IApplicationService<AuthorDto, AuthorCreateUpdateDto, int>
{
    /// <summary>
    /// Получает коллекцию связанных сущностей
    /// </summary>
    /// <param name="dtoId">Идентификатор автора</param>
    /// <returns>Список связей</returns>
    List<BookAuthorDto> GetBookAuthors(int dtoId);
}
